using ControlaAiBack.Application.Autentication;
using ControlaAiBack.Application.DTOs;
using ControlaAiBack.Application.Interfaces;
using ControlaAiBack.Domain.Entities;
using ControlaAiBack.Domain.Interfaces;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto, Users.UserType userType)
    {
        var user = new Users
        {
            NomeEmpresa = userCreateDto.NomeEmpresa,
            Nome = userCreateDto.Nome,
            Email = userCreateDto.Email,
            SenhaHash = PasswordHelper.HashPassword(userCreateDto.Senha),
            Permissao = userType
        };

        await _userRepository.AddAsync(user);

        return new UserDto
        {
            Id = user.Id,
            NomeEmpresa = user.NomeEmpresa,
            Nome = user.Nome,
            Email = user.Email,
            Permissao = user.Permissao
        };
    }

    public async Task<UserDto> CreateAdminUserAsync(UserCreateDto userCreateDto)
    {
        return await CreateUserAsync(userCreateDto, Users.UserType.Admin);
    }

    public async Task<bool> SoftDeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null || user.IsDeleted) return false;

        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> RestoreUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null || !user.IsDeleted) return false;

        user.IsDeleted = false;
        user.DeletedAt = null;
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<UserDto> CreateUserByAdminAsync(UserCreateDto userCreateDto, Guid adminId)
    {
        var nomeEmpresa = await GetCompanyNameByAdminIdAsync(adminId);

        var user = new Users
        {
            NomeEmpresa = nomeEmpresa,
            Nome = userCreateDto.Nome,
            Email = userCreateDto.Email,
            SenhaHash = PasswordHelper.HashPassword(userCreateDto.Senha),
            Permissao = Users.UserType.Funcionario
        };

        await _userRepository.AddAsync(user);

        return new UserDto
        {
            Id = user.Id,
            NomeEmpresa = user.NomeEmpresa,
            Nome = user.Nome,
            Email = user.Email,
            Permissao = user.Permissao
        };
    }

    public async Task<string?> GetCompanyNameByAdminIdAsync(Guid adminId) 
    {
        var user = await _userRepository.GetByIdAsync(adminId);
        return user?.NomeEmpresa; 
    }
}
