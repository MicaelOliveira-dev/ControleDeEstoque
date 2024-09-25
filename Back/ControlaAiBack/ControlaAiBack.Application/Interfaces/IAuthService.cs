using ControlaAiBack.Application.DTOs;

namespace ControlaAiBack.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> LoginAsync(LoginDto loginDto);
    }
}
