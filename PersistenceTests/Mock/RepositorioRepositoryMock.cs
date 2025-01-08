using Microsoft.EntityFrameworkCore;
using System;
using Persistence.InMemory.Entities;
using Persistence.InMemory.Repositories;
using Persistence.InMemory;

namespace SystemTests.Mock;
public class RepositorioRepositoryMock : IRepositorioRepository {
    private InMemoryDbContext _dbContext;
    public RepositorioRepositoryMock() {
        var options = new DbContextOptionsBuilder<InMemoryDbContext>()
           .UseInMemoryDatabase(databaseName: "TesteInMemory")
           .Options;

        _dbContext = new InMemoryDbContext(options);
        _dbContext.Database.EnsureDeleted();


        var mockRep1 = new Repositorio("Repositorio de Teste 1 ALFA", "Repositorio feito pelo mock", "C#", "Sistema", DateTime.Now);
        var mockRep2 = new Repositorio("Repositorio de Teste 2 ALFA", "Repositorio feito pelo mock", "C#", "Sistema", DateTime.Now);
        var mockRep3 = new Repositorio("Repositorio de Teste 3 ALFA", "Repositorio feito pelo mock", "C#", "Sistema", DateTime.Now);
        var mockRep4 = new Repositorio("Repositorio de Teste 4 BETA", "Repositorio feito pelo mock", "C#", "Sistema", DateTime.Now);
        var mockRep5 = new Repositorio("Repositorio de Teste 5 BETA", "Repositorio feito pelo mock", "C#", "Sistema", DateTime.Now);
        var mockRep6 = new Repositorio("Repositorio de Teste 6 BETA", "Repositorio feito pelo mock", "C#", "Sistema", DateTime.Now);

        mockRep1.DefinirFavorito();
        mockRep3.DefinirFavorito();
        mockRep4.DefinirFavorito();
        mockRep5.DefinirFavorito();

        _dbContext.Add(mockRep1);
        _dbContext.Add(mockRep2);
        _dbContext.Add(mockRep3);
        _dbContext.Add(mockRep4);
        _dbContext.Add(mockRep5);
        _dbContext.Add(mockRep6);

        _dbContext.SaveChanges();
    }

    public async Task<Repositorio> Create(Repositorio newRepository) {
        _dbContext.Repositorios.Add(newRepository);
        await _dbContext.SaveChangesAsync();

        return newRepository;
    }
    public async Task<Repositorio> Update(Repositorio newRepository) {
        _dbContext.Repositorios.Update(newRepository);
        await _dbContext.SaveChangesAsync();
        return newRepository;
    }

    public async Task<int> Delete(Repositorio repository) {
        _dbContext.Repositorios.Remove(repository);
        var result = await _dbContext.SaveChangesAsync();
        return result;
    }

    public async Task<IList<Repositorio>> GetAll() =>
        await _dbContext.Repositorios.ToListAsync();


    public async Task<IList<Repositorio>> GetAllByName(string query) =>
        await _dbContext.Repositorios.Where(x => x.Nome.Contains(query)).ToListAsync();


    public async Task<IList<Repositorio>> GetAllFavorites() =>
        await _dbContext.Repositorios.Where(x => x.Favorito == true).ToListAsync();

    public async Task<Repositorio?> GetById(int? Id)
        => await _dbContext.Repositorios.FindAsync(Id);
}
