using ControlaAiBack.Domain.Entities;

namespace ControlaAiBack.Application.DTOs
{
    namespace ControlaAiBack.Application.Dtos
    {
        public class UserCreateDto
        {
            public string NomeEmpresa { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public Users.UserType Permissao { get; set; }
        }
    }

}
