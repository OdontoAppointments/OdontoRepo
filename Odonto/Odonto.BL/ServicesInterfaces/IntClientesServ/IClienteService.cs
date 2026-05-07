using Odonto.BL.DTO.ClienteDTO.Request;
using Odonto.BL.DTO.ClienteDTO.Response;

namespace Odonto.BL.ServicesInterfaces.IntClientesServ;

public interface IClienteService
{
    Task<ClienteResponse> ObterPorIdAsync(int id);
    Task<IEnumerable<ClienteResponse>> ListarAsync();
    Task<ClienteResponse> CriarAsync(CriarClienteRequest request);
    Task<ClienteResponse> AtualizarAsync(int id, AtualizarClienteRequest request);
    Task RemoverAsync(int id);
}