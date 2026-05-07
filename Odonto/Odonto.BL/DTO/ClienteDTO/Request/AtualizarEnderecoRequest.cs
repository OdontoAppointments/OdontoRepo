namespace Odonto.BL.DTO.ClienteDTO.Request;

public record AtualizarEnderecoRequest(
    string? Rua,
    int Numero,
    string? Bairro,
    string? CEP,
    string? Estado,
    string? Cidade
);