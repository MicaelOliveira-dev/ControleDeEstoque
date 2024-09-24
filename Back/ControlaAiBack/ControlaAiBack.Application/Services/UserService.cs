using ControlaAiBack.Application.Autentication;
using ControlaAiBack.Application.DTOs.ControlaAiBack.Application.Dtos;
using ControlaAiBack.Application.Interfaces;
using ControlaAiBack.Domain.Entities;
using ControlaAiBack.Domain.Interfaces;

namespace ControlaAiBack.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateAdminUserAsync(UserCreateDto userCreateDto)
        {
            var user = new User
            {
                NomeEmpresa = userCreateDto.NomeEmpresa,
                NomeProprietario = userCreateDto.NomeProprietario,
                Email = userCreateDto.Email,
                SenhaHash = PasswordHelper.HashPassword(userCreateDto.Senha),
                Permissao = User.UserType.Admin
            };

            await _userRepository.AddAsync(user);
            return user;
        }
    }
}
