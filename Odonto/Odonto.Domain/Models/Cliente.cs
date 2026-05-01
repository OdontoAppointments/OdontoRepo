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
}