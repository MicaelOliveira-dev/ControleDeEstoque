using System;

namespace ControlaAiBack.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid userId)
            : base($"Usuário com ID {userId} não foi encontrado.")
        {
        }
    }

    public class UserCreationException : Exception
    {
        public UserCreationException(string message)
            : base($"Falha na criação do usuário: {message}")
        {
        }
    }

    public class InvalidUserFieldsException : Exception
    {
        public InvalidUserFieldsException(string message)
            : base(message)
        {
        }
    }
}
