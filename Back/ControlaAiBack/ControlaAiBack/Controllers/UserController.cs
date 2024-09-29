﻿using ControlaAiBack.Application.DTOs;
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

        [HttpPost("CreateAdmin")]
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

        [HttpPost("{adminId}/CreateUser")]
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

        [HttpPost("{adminId}/CreateUsersFromFile")]
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

        [HttpGet("{companyName}/ExportUsers")]
        public async Task<IActionResult> ExportUsers(string companyName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            try
            {
                var users = await _userService.GetUsersByCompanyNameAsync(companyName);
                if (users == null || !users.Any())
                {
                    return NotFound("Nenhum usuário encontrado para este admin.");
                }

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Usuários");
                    worksheet.Cells[1, 1].Value = "Nome";
                    worksheet.Cells[1, 2].Value = "NomeEmpresa";
                    worksheet.Cells[1, 3].Value = "Email";
                    worksheet.Cells[1, 4].Value = "Permissao";

                    for (int i = 0; i < users.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = users[i].Nome;
                        worksheet.Cells[i + 2, 2].Value = users[i].NomeEmpresa;
                        worksheet.Cells[i + 2, 3].Value = users[i].Email;
                        worksheet.Cells[i + 2, 4].Value = users[i].Permissao;
                    }

                    var stream = new MemoryStream();
                    await package.SaveAsAsync(stream);
                    stream.Position = 0;

                    var fileName = $"Usuarios_{companyName}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao gerar a planilha: {ex.Message}");
            }
        }


        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> SoftDeleteUser(Guid id)
        {
            var result = await _userService.SoftDeleteUserAsync(id);
            if (!result)
                return NotFound(); 

            return NoContent();
        }

        [HttpPut("RestoreUser/{id}")]
        public async Task<IActionResult> RestoreUser(Guid id)
        {
            var result = await _userService.RestoreUserAsync(id);
            if (!result)
                return NotFound(); 

            return NoContent();
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
