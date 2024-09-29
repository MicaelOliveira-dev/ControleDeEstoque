namespace ControlaAiBack.Application.Exceptions
{
    public class InvalidFileFormatException : Exception
    {
        public InvalidFileFormatException(string message)
            : base($"Formato de arquivo inválido: {message}")
        {
        }        
    }

    public class InvalidFileException : Exception
    {
        public InvalidFileException(string message)
            : base($"{message}")
        {
        }
    }
}
