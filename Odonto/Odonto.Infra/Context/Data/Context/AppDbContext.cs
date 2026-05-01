using Microsoft.EntityFrameworkCore;
using Odonto.Domain.Models;


namespace Odonto.Infra.Context.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Cliente> Clientes  { get; set; }
}