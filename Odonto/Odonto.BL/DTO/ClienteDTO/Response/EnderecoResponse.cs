namespace Odonto.BL.DTO.ClienteDTO.Response;

public record EnderecoResponse(
    int Id,
    string Rua,
    int Numero,
    string Bairro,
    string CEP,
    string Estado,
    string Cidade
);