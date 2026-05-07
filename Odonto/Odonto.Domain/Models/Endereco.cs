namespace Odonto.Domain.Models;

public class Endereco
{
    public int Id { get; set; }
    public string Rua { get; set; }
    public int Numero { get; set; }
    public string Bairro { get; set; }
    public string CEP { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
    
    public Endereco(
        string rua,
        int numero,
        string bairro,
        string cep,
        string estado,
        string cidade)
    {
        Rua = rua;
        Numero = numero;
        Bairro = bairro;
        CEP = cep;
        Estado = estado;
        Cidade = cidade;
    }

    public void Atualizar(
        string? rua,
        int? numero,
        string? bairro,
        string? cep,
        string? estado,
        string? cidade)
    {
        if (!string.IsNullOrWhiteSpace(rua))
            Rua = rua;

        if (numero.HasValue)
            Numero = numero.Value;

        if (!string.IsNullOrWhiteSpace(bairro))
            Bairro = bairro;

        if (!string.IsNullOrWhiteSpace(cep))
            CEP = cep;

        if (!string.IsNullOrWhiteSpace(estado))
            Estado = estado;

        if (!string.IsNullOrWhiteSpace(cidade))
            Cidade = cidade;
    }
}