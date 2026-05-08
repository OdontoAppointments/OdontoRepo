using Odonto.BL.DTO.ClienteDTO.Request;
using Odonto.BL.DTO.ClienteDTO.Response;
using Odonto.Domain.Models;

namespace Odonto.BL.ServicesInterfaces.IntClientesServ;

public interface IClienteService
{
    Task<Result<ClienteResponse>> ObterPorIdAsync(int id);
    Task<Result<ClienteResponse>> ObterPorNomeAsync(string nome);
    Task<Result<IEnumerable<ClienteResponse>>> ListarAsync();
    Task<Result<ClienteResponse>> CriarAsync(CriarClienteRequest request);
    Task<Result<ClienteResponse>> AtualizarAsync(int id, AtualizarClienteRequest request);
    Task<Result<bool>> RemoverAsync(int id);
}