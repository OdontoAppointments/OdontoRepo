using System.Data;

namespace Odonto.Domain.Models;

public class Cliente
{
    public int Id  { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public Endereco Endereco { get; set; }
    public DateOnly DataNascimento { get; set; }
    public DateTime CriadoEm  { get; set; } = DateTime.UtcNow;

    public Cliente(string nome, string email, string telefone, Endereco endereco, DateOnly dataNascimento)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Endereco = endereco;
        DataNascimento = dataNascimento;
    }
    public void Atualizar(string? nome, string? email, string? telefone, DateOnly? dataNascimento)
    {
        if (!string.IsNullOrWhiteSpace(nome))     Nome     = nome;
        if (!string.IsNullOrWhiteSpace(email))    Email    = email;
        if (!string.IsNullOrWhiteSpace(telefone)) Telefone = telefone;
        if (dataNascimento.HasValue)              DataNascimento = dataNascimento.Value;
    }
}