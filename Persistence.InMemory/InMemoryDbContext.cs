using Microsoft.EntityFrameworkCore;
using Persistence.InMemory.Entities;

namespace Persistence.InMemory;

public class InMemoryDbContext : DbContext {
    public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options) { }
    public DbSet<Repositorio> Repositorios { get; set; }
}