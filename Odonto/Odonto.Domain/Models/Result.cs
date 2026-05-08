namespace Odonto.Domain.Models;

public class Result<T>
{
    public bool Sucesso { get; private set; }
    public string Mensagem { get; private set; }
    public T? Dados { get; private set; }

    private Result() { }

    public static Result<T> Ok(T dados, string mensagem = "Operação realizada com sucesso.") =>
        new() { Sucesso = true, Dados = dados, Mensagem = mensagem };

    public static Result<T> Falha(string mensagem) =>
        new() { Sucesso = false, Mensagem = mensagem };
}