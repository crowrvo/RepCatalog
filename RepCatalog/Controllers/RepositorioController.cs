using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Persistence.InMemory.Entities;
using Persistence.InMemory.Repositories;
using RepCatalog.Abstractions;
using RepCatalog.Commands;
using RepCatalog.Commands;

namespace RepCatalog.Controllers;
[ApiController]
[Route("api/[controller]")]

public class RepositorioController : Controller {

    private readonly RepositorioRepository _dbRepository;
    private readonly ILogger<RepositorioController> _logger;

    public RepositorioController(RepositorioRepository dbRepository, ILogger<RepositorioController> logger) {
        _dbRepository = dbRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? nome) {
        if (nome == null) {
            var Repositories = await _dbRepository.GetAll();
            return Ok(Result.Success(Repositories));
        }

        var QueriedRepositories = await _dbRepository.GetAllByName(nome);
        return Ok(Result.Success(QueriedRepositories));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRepositoryCommand createRepositoryCommand) {
        try {

            if (!createRepositoryCommand.IsValid)
                return BadRequest(Result.Failure(RepositorioErrors.BadRequest, createRepositoryCommand.Notifications));

            var repository = new Repositorio(
                nome: createRepositoryCommand.Nome,
                descricao: createRepositoryCommand.Descricao,
                linguagem: createRepositoryCommand.Linguagem,
                donoDoRepositorio: createRepositoryCommand.DonoDoRepositorio,
                ultimaAtualizacao: DateTime.Parse(createRepositoryCommand.UltimaAtualizacao)
            );

            var Repositories = await _dbRepository.Create(repository);

            return CreatedAtAction(nameof(Create), new { id = repository.Id }, Result.Success(repository));
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) {
        // Aqui poderia ser um tenário mas prefiro a legibilidade que o escopo trás.
        var repository = await _dbRepository.GetById(id);
        if (repository == null) {
            _logger.LogWarning($"Falha ao atualizar o repositório {id}: Repositório não encontrado!");
            return NotFound(Result.Failure(RepositorioErrors.NotFound));
        }

        return Ok(repository);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateById(int id, UpdateRepositoryCommand updateRepositoryCommand) {

        if (!updateRepositoryCommand.IsValid)
            return BadRequest(Result.Failure(RepositorioErrors.BadRequest, updateRepositoryCommand.Notifications));

        var repository = await _dbRepository.GetById(id);
        if (repository == null) {
            _logger.LogWarning($"Falha ao atualizar o repositório {id}: Repositório não encontrado!");
            return NotFound(Result.Failure(RepositorioErrors.NotFound));
        }

        repository.Update(
            updateRepositoryCommand.Nome,
            updateRepositoryCommand.Descricao,
            updateRepositoryCommand.Linguagem,
            updateRepositoryCommand.DonoDoRepositorio,
            DateTime.Parse(updateRepositoryCommand.UltimaAtualizacao)
        );


        var result = await _dbRepository.Update(repository);
        _logger.LogDebug($"Repositório {id} atualizado com sucesso!");
        return Ok(Result.Success(result));
    }


    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteById(int id) {
        var repository = await _dbRepository.GetById(id);
        if (repository == null) {
            _logger.LogWarning($"Falha ao deletar o repositório {id}: Repositório não encontrado!");
            return NotFound(Result.Failure(RepositorioErrors.NotFound));
        }

        if (repository.Favorito) {
            _logger.LogWarning($"Falha ao deletar o repositório {id}: Repositório marcado como favorito!");
            return StatusCode(405, Result.Failure(RepositorioErrors.NotAllowedDeleteFavorite));
        }

        var result = await _dbRepository.Delete(repository);

        _logger.LogDebug($"Repositório {id} deletado com sucesso!");
        return Ok(Result.Success("Repositório deletado com sucesso!"));
    }
}
