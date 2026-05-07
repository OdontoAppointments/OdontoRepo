namespace Odonto.BL.DTO.ClienteDTO.Request;

public record EnderecoRequest(
    string Rua,
    int Numero,
    string Bairro,
    string CEP,
    string Estado,
    string Cidade
);