

namespace ControlaAiBack.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();  
        public string NomeEmpresa { get; set; }
        public string NomeProprietario { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public UserType Permissao { get; set; }

        public enum UserType 
        {
            Admin,
            Funcionario
        }
    }

}
