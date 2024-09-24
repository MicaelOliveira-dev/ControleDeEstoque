using ControlaAiBack.Application.DTOs.ControlaAiBack.Application.Dtos;
using ControlaAiBack.Domain.Entities;

namespace ControlaAiBack.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateAdminUserAsync(UserCreateDto userCreateDto);
    }
}
