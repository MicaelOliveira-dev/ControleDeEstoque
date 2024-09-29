using ControlaAiBack.Application.DTOs;
using ControlaAiBack.Domain.Entities;

namespace ControlaAiBack.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto, Users.UserType userType);
        Task<UserDto> CreateAdminUserAsync(UserCreateDto userCreateDto);
        Task<bool> SoftDeleteUserAsync(Guid id);
        Task<bool> RestoreUserAsync(Guid id);
        Task<UserDto> CreateUserByAdminAsync(UserCreateDto userCreateDto, Guid adminId);
        Task<List<UserDto>> GetUsersByCompanyNameAsync(string companyName);
        Task<string?> GetCompanyNameByAdminIdAsync(Guid adminId);
    }
}
