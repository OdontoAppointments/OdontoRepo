namespace Odonto.Domain.Exceptions.Clientes;

public class DadosInvalidosException : Exception
{
    public DadosInvalidosException(string mensagem)
        : base(mensagem) { }
}