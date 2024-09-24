using ControlaAiBack.Domain.Entities;

namespace ControlaAiBack.Application.DTOs
{
    namespace ControlaAiBack.Application.Dtos
    {
        public class UserCreateDto
        {
            public string NomeEmpresa { get; set; }
            public string NomeProprietario { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public User.UserType Permissao { get; set; }
        }
    }

}
