using Microsoft.EntityFrameworkCore;
using Odonto.Domain.Models;
using Odonto.Infra.Context.Data.Context;
using Odonto.Infra.RepositoryInterfaces.IClienteRepo;

namespace Odonto.Infra.Repositories.ClienteRepo;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Cliente>> ObterTodos()
    {
        return await _context.Clientes
            .Include((c => c.Endereco))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Cliente?> ObterPorId(int id)
    {
        return await _context.Clientes
            .Include((c => c.Endereco))
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cliente?> ObterPorNome(string nome)
    {
        
        return await _context.Clientes
            .Include((c => c.Endereco))
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Nome == nome);
    }

    public async Task<Cliente?> ObterPorEmail(string email)
    {
        return await _context.Clientes
            .Include((c => c.Endereco))
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Cliente> Adicionar(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> Atualizar(Cliente cliente)
    {
        var clienteExistente = await  _context.Clientes
            .Include((c => c.Endereco))
            .FirstOrDefaultAsync(c => c.Id == cliente.Id);
        
        _context.Entry(clienteExistente).CurrentValues.SetValues(cliente);

        if (cliente.Endereco is not null)
        {
            if (clienteExistente!.Endereco is null)
            {
                clienteExistente.Endereco = cliente.Endereco;
            }
            else
            {
                _context.Entry(clienteExistente.Endereco).CurrentValues.SetValues(cliente.Endereco);
            }
        }
        
        
        await _context.SaveChangesAsync();
        return  clienteExistente!;
    }

    public async Task Remover(int id)
    {
        var clienteExistente = _context.Clientes
            .FirstOrDefault(c => c.Id == id);
        
        _context.Clientes.Remove(clienteExistente);
        await _context.SaveChangesAsync();
    }
}