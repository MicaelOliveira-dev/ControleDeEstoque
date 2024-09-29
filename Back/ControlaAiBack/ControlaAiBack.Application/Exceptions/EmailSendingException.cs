
namespace ControlaAiBack.Application.Exceptions
{

    public class EmailSendingException : Exception
    {
        public EmailSendingException(string email, Exception innerException)
            : base($"Falha ao enviar e-mail para {email}: {innerException.Message}", innerException)
        {
        }
    }
}
