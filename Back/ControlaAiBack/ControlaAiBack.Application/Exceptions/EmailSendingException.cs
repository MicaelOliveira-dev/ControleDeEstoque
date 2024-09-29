using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlaAiBack.Application.Exceptions
{
    public class EmailSendingException : Exception
    {
        public EmailSendingException(string email, Exception innerException)
            : base($" {email}: {innerException.Message}", innerException)
        {
        }
    }
}
