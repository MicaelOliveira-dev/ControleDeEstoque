using ControlaAiBack.Application.DTOs;
using ControlaAiBack.Application.DTOs.ControlaAiBack.Application.Dtos;
using ControlaAiBack.Application.Exceptions;
using ControlaAiBack.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

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
    }
}
