namespace Odonto.Domain.Exceptions.Clientes;

public class ClienteNaoEncontradoException : Exception
{
    public ClienteNaoEncontradoException(int id)
        : base($"Cliente com Id '{id}' não foi encontrado.") { }
}