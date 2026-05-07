namespace Odonto.Domain.Exceptions.Clientes;

public class EmailJaCadastradoException : Exception
{
    public EmailJaCadastradoException(string email)
        : base($"O e-mail '{email}' já está cadastrado.") { }
}