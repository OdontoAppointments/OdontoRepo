namespace Odonto.BL.DTO.ClienteDTO.Response;

public record ClienteResponse(
    int Id,
    string Nome,
    string Email,
    string Telefone,
    EnderecoResponse Endereco,
    DateOnly DataNascimento,
    DateTime DataCriacao
);