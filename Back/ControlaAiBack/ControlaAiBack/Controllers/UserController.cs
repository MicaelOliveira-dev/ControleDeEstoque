using ControlaAiBack.Application.DTOs;
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
            {
                return BadRequest(ModelState);
            }

            if (!IsUserCreateDtoValid(userCreateDto))
            {
                return BadRequest("Um ou mais campos do usuário estão inválidos.");
            }

            try
            {
                var user = await _userService.CreateAdminUserAsync(userCreateDto);
                await SendWelcomeEmail(user, userCreateDto);

                return CreatedAtAction(nameof(CreateAdminUser), new { id = user.Id }, user);
            }
            catch (UserCreationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-user/{adminId}")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto, Guid adminId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!IsUserCreateDtoValid(userCreateDto))
            {
                return BadRequest("Um ou mais campos do usuário estão inválidos.");
            }

            try
            {
                var user = await _userService.CreateUserByAdminAsync(userCreateDto, adminId);
                await SendWelcomeEmail(user, userCreateDto);

                return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
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
                return NotFound(); 

            return NoContent();
        }

        [HttpPut("restore-user/{id}")]
        public async Task<IActionResult> RestoreUser(Guid id)
        {
            var result = await _userService.RestoreUserAsync(id);
            if (!result)
                return NotFound(); 

            return NoContent();
        }

        [HttpPost("create-users-using-excel/{adminId}")]
        public async Task<IActionResult> CreateUsersFromFile([FromRoute] Guid adminId, IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (file == null || file.Length == 0)
                return BadRequest("Arquivo inválido.");

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
                            return NotFound("Nome da empresa não encontrado."); 
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

                            if (!IsUserCreateDtoValid(userCreateDto))
                            {
                                return BadRequest("Um ou mais campos do usuário estão inválidos.");
                            }

                            if (!await _emailService.IsValidEmail(userCreateDto.Email))
                            {
                                return BadRequest($"O endereço de e-mail '{userCreateDto.Email}' fornecido não é válido.");
                            }

                            var user = await _userService.CreateUserByAdminAsync(userCreateDto, adminId);
                            await SendWelcomeEmail(user, userCreateDto);
                        }
                    }
                }

                return Ok(new { message = "Usuários criados com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro no formato do arquivo: {ex.Message}"); 
            }
        }

        private bool IsUserCreateDtoValid(UserCreateDto userCreateDto)
        {
            return !string.IsNullOrEmpty(userCreateDto.Nome) &&
                   !string.IsNullOrEmpty(userCreateDto.Senha) &&
                   !string.IsNullOrEmpty(userCreateDto.Email);
        }

        private async Task SendWelcomeEmail(UserDto user, UserCreateDto userCreateDto)
        {
            var emailDto = new EmailDto
            {
                Para = userCreateDto.Email,
                Nome = userCreateDto.NomeEmpresa,
                Senha = userCreateDto.Senha,
                Assunto = "Bem-vindo ao nosso serviço ControlaAí!"
            };

            try
            {
                await _emailService.sendEmail(emailDto); 
            }
            catch (Exception emailEx)
            {
                throw new EmailSendingException(userCreateDto.Email, emailEx);
            }
        }
    }
}
