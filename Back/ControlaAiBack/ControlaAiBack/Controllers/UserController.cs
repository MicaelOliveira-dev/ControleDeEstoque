using ControlaAiBack.Application.DTOs;
using ControlaAiBack.Application.DTOs.ControlaAiBack.Application.Dtos;
using ControlaAiBack.Application.Exceptions;
using ControlaAiBack.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace ControlaAiBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public UsersController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdminUser([FromBody] UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _userService.CreateAdminUserAsync(userCreateDto);

                var emailDto = new EmailDto
                {
                    Para = userCreateDto.Email,
                    Nome = userCreateDto.NomeEmpresa,
                    Senha = userCreateDto.Senha,
                    Assunto = "Bem-vindo ao nosso serviço ControlaAí!"
                };

                _emailService.sendEmail(emailDto);

                return CreatedAtAction(nameof(CreateAdminUser), new { id = user.Id }, user);
            }
            catch (UserCreationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> SoftDeleteUser(Guid id)
        {
            var result = await _userService.SoftDeleteUserAsync(id);
            if (!result)
                throw new UserNotFoundException(id);

            return NoContent();
        }

        [HttpPut("restore-user/{id}")]
        public async Task<IActionResult> RestoreUser(Guid id)
        {
            var result = await _userService.RestoreUserAsync(id);
            if (!result)
                throw new UserNotFoundException(id);

            return NoContent();
        }

        [HttpPost("create-users/{adminId}")]
        public async Task<IActionResult> CreateUsersFromFile([FromRoute] Guid adminId, IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (file == null || file.Length == 0)
                throw new InvalidFileException("Arquivo inválido.");

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        var nomeEmpresa = await _userService.GetCompanyNameByAdminIdAsync(adminId);

                        if (string.IsNullOrEmpty(nomeEmpresa))
                        {
                            throw new CompanyNameNotFoundException(adminId);
                        }

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var userCreateDto = new UserCreateDto
                            {
                                NomeEmpresa = nomeEmpresa,
                                Nome = worksheet.Cells[row, 1].Text,
                                Email = worksheet.Cells[row, 2].Text,
                                Senha = worksheet.Cells[row, 3].Text
                            };

                            if (string.IsNullOrEmpty(userCreateDto.NomeEmpresa) ||
                                string.IsNullOrEmpty(userCreateDto.Nome) ||
                                string.IsNullOrEmpty(userCreateDto.Email) ||
                                string.IsNullOrEmpty(userCreateDto.Senha))
                            {
                                throw new InvalidUserFieldsException("Um ou mais campos do usuário estão inválidos.");
                            }

                            var user = await _userService.CreateUserAsync(userCreateDto, adminId);

                            var emailDto = new EmailDto
                            {
                                Para = userCreateDto.Email,
                                Nome = userCreateDto.Nome,
                                Senha = userCreateDto.Senha,
                                Assunto = "Bem-vindo ao nosso serviço ControlaAí!"
                            };

                            _emailService.sendEmail(emailDto);
                        }
                    }
                }

                return Ok(new { message = "Usuários criados com sucesso!" });
            }
            catch (Exception ex)
            {
                throw new InvalidFileFormatException($"{ex.Message}");
            }
        }
    }
}

