
namespace ControlaAiBack.Domain.Entities
{
    public class Users
    {
        public Guid Id { get; set; } = Guid.NewGuid();  
        public string NomeEmpresa { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public UserType Permissao { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public enum UserType 
        {
            Admin,
            Funcionario
        }
    }

}
