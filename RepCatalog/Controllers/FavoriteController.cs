using Microsoft.AspNetCore.Mvc;
using Persistence.InMemory.Repositories;
using RepCatalog.Abstractions;

namespace RepCatalog.Controllers;

[ApiController]
[Route("api/repositorio/[controller]")]
public class FavoriteController : Controller {

    private readonly RepositorioRepository _dbRepository;
    private readonly ILogger<FavoriteController> _logger;

    public FavoriteController(RepositorioRepository dbRepository, ILogger<FavoriteController> logger) {
        _dbRepository = dbRepository;
        _logger = logger;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllFavorites() {
        var Repositories = await _dbRepository.GetAllFavorites();
        return Ok(Result.Success(Repositories));
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> SetFavorite(int id) {
        var repository = await _dbRepository.GetById(id);
        if (repository == null)
            return NotFound(Result.Failure("Repositório não encontrado"));

        if (repository.Favorito)
            return StatusCode(405, Result.Failure("Esse repositório já está marcado como favorito!"));

        repository.DefinirFavorito();
        var result = await _dbRepository.Update(repository);

        _logger.LogDebug($"Repositório {id} Favoritado com sucesso!");
        return Ok(Result.Success("Repositório Favoritado com sucesso!"));
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> UnsetFavorite(int id) {
        var repository = await _dbRepository.GetById(id);
        if (repository == null)
            return NotFound();

        if (!repository.Favorito)
            return StatusCode(405, Result.Failure("Esse repositório não está marcado como favorito!"));

        repository.RemoverFavorito();
        var result = await _dbRepository.Update(repository);

        _logger.LogDebug($"Repositório {id} Desfavoritado com sucesso!");
        return Ok(Result.Success("Repositório Desfavoritado com sucesso!"));
    }
}
