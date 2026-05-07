namespace Odonto.BL.DTO.ClienteDTO.Request;

public record AtualizarClienteRequest(
    string? Nome,
    string? Email,
    string? Telefone,
    AtualizarEnderecoRequest? Endereco,
    DateOnly? DataNascimento
);