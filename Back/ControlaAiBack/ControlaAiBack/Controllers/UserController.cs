using ControlaAiBack.Application.DTOs;
using ControlaAiBack.Application.DTOs.ControlaAiBack.Application.Dtos;
using ControlaAiBack.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlaAiBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdminUser([FromBody] UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.CreateAdminUserAsync(userCreateDto);
            return CreatedAtAction(nameof(CreateAdminUser), new { id = user.Id }, user);
        }
    }
}
