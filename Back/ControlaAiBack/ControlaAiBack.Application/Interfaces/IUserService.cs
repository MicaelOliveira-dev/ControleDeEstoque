using ControlaAiBack.Application.DTOs;
using ControlaAiBack.Application.DTOs.ControlaAiBack.Application.Dtos;

namespace ControlaAiBack.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateAdminUserAsync(UserCreateDto userCreateDto);
        Task<bool> SoftDeleteUserAsync(Guid id);
        Task<bool> RestoreUserAsync(Guid id);
    }
}
