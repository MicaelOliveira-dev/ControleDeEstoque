using ControlaAiBack.Domain.Entities;

namespace ControlaAiBack.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string NomeEmpresa { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Users.UserType Permissao { get; set; }
    }

}
