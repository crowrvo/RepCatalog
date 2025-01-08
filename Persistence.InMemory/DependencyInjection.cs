using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.InMemory.Repositories;

namespace Persistence.InMemory;

public static class DependencyInjection {
    public static IServiceCollection AddPersistenceInMemory(this IServiceCollection services) {

        services.AddDbContext<InMemoryDbContext>(opt => opt.UseInMemoryDatabase("RepsDatabase"));
        services.AddScoped<RepositorioRepository>();

        return services;
    }
}