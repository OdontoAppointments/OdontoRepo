using Odonto.Domain.Models;

namespace Odonto.Infra.RepositoryInterfaces.IClienteRepo;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObterTodos();
    Task<Cliente> ObterPorId(int id);
    Task<Cliente> ObterPorNome(string nome);
    Task<Cliente> ObterPorEmail(string email);
    Task<Cliente> Adicionar(Cliente cliente);
    Task<Cliente> Atualizar(Cliente cliente);
    Task Remover(int id);
}