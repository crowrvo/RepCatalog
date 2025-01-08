using Persistence.InMemory.Entities;

namespace Persistence.InMemory.Repositories;
public interface IRepositorioRepository {
    Task<Repositorio> Create(Repositorio newRepository);
    Task<int> Delete(Repositorio repository);
    Task<IList<Repositorio>> GetAll();
    Task<IList<Repositorio>> GetAllByName(string query);
    Task<IList<Repositorio>> GetAllFavorites();
    Task<Repositorio?> GetById(int? Id);
    Task<Repositorio> Update(Repositorio newRepository);
}