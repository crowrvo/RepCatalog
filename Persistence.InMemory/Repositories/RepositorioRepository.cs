using Microsoft.EntityFrameworkCore;
using Persistence.InMemory.Entities;

namespace Persistence.InMemory.Repositories;

public class RepositorioRepository : IRepositorioRepository {

    private InMemoryDbContext _InMemoryDbContext;

    public RepositorioRepository(InMemoryDbContext InMemoryDbContext) {
        _InMemoryDbContext = InMemoryDbContext;
    }

    public async Task<IList<Repositorio>> GetAll()
        => await _InMemoryDbContext.Repositorios.ToListAsync();

    public async Task<IList<Repositorio>> GetAllByName(string query)
        => await _InMemoryDbContext.Repositorios.Where(x => x.Nome.Contains(query)).ToListAsync();

    public async Task<IList<Repositorio>> GetAllFavorites()
        => await _InMemoryDbContext.Repositorios.Where(x => x.Favorito == true).ToListAsync();

    public async Task<Repositorio?> GetById(int? Id)
        => await _InMemoryDbContext.Repositorios.FindAsync(Id);

    public async Task<Repositorio> Create(Repositorio newRepository) {
        await _InMemoryDbContext.AddAsync(newRepository);
        await _InMemoryDbContext.SaveChangesAsync();
        return newRepository;
    }

    public async Task<Repositorio> Update(Repositorio newRepository) {
        _InMemoryDbContext.Update(newRepository);
        await _InMemoryDbContext.SaveChangesAsync();
        return newRepository;
    }

    public async Task<int> Delete(Repositorio repository) {
        _InMemoryDbContext.Remove(repository);
        return await _InMemoryDbContext.SaveChangesAsync();
    }

}