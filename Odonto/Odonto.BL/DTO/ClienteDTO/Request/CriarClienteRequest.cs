namespace Odonto.BL.DTO.ClienteDTO.Request;

public record CriarClienteRequest(
    string Nome,
    string Email,
    string Telefone,
    EnderecoRequest Endereco,
    DateOnly DataNascimento
);