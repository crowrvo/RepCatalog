using Persistence.InMemory.Entities;
using Persistence.InMemory.Repositories;
using SystemTests.Mock;

namespace SystemTests;

[TestClass]
public class RepositorioReposityTest {

    private IRepositorioRepository mockRepository;

    public RepositorioReposityTest() {
        mockRepository = new RepositorioRepositoryMock();
    }

    [TestCleanup]
    public void reset() {
        mockRepository = new RepositorioRepositoryMock();
    }

    [TestMethod]
    public async Task ItShouldCreateARepositorio() {
        var newRepo = new Repositorio("Repositorio de Teste", "Repositorio feito pelo mock", "C#", "Sistema", DateTime.Now);
        var result = await mockRepository.Create(newRepo);

        Assert.IsNotNull(result.Id, "É esperado que um ID seja inserido dentro do objeto em caso de sucesso na criação da linha no banco!");
    }

    [TestMethod]
    public async Task ItShouldFindARepositorioById() {
        const string EXPECTED_VALUE = "Repositorio de Teste";
        var newRepo = new Repositorio("Repositorio de Teste", "Repositorio feito pelo mock", "C#", "Sistema", DateTime.Now);
        var result = await mockRepository.Create(newRepo);

        var stu = await mockRepository.GetById(result.Id);

        Assert.AreEqual(EXPECTED_VALUE, stu.Nome, "É esperado que o item recem adicionado seja recuperado pelo ID.");

    }

    [TestMethod]
    public async Task ItShouldUpdateARepositorio() {
        const string EXPECTED_VALUE_1 = "Repositorio de Teste";
        const string EXPECTED_VALUE_2 = "Nome Editado";
        var newRepo = new Repositorio("Repositorio de Teste", "Repositorio feito pelo mock", "C#", "Sistema", DateTime.Now);
        var createdRepo = await mockRepository.Create(newRepo);
        createdRepo.Update(
            "Nome Editado",
            createdRepo.Descricao,
            createdRepo.Linguagem,
            createdRepo.DonoDoRepositorio,
            createdRepo.UltimaAtualizacao
        );
        var UpdatedRepo = await mockRepository.Update(createdRepo);
        var RecoveredRepo = await mockRepository.GetById(newRepo.Id);

        Assert.AreEqual(EXPECTED_VALUE_2, UpdatedRepo.Nome, "É esperado que o nome do repositório Atualizado tenha atualizado!");
        Assert.AreEqual(EXPECTED_VALUE_2, RecoveredRepo.Nome, "É esperado que o nome do repositório Atualizado, recuperado pelo ID inicial tenha atualizado!");
    }

    [TestMethod]
    public async Task ItShouldDeleteARepositorio() {
        var newRepo = new Repositorio("Repositorio de Teste", "Repositorio feito pelo mock", "C#", "Sistema", DateTime.Now);
        var createdRepo = await mockRepository.Create(newRepo);
        await mockRepository.Delete(createdRepo);
        var stu = await mockRepository.GetById(createdRepo.Id);

        Assert.IsNull(stu, "É esperado que o repositório seja nulo após ter sido deletado!");
    }


    [TestMethod]
    public async Task ItShouldReturn3ItensFromGetAllByQuery() {
        const int EXPECTED_VALUE = 3;
        var itens = await mockRepository.GetAllByName("BETA");

        foreach (var item in itens)
            Assert.IsTrue(item.Nome.Contains("BETA"), "É esperado que apenas repositórios que tenham BETA escrito estejam na lista");

        Assert.AreEqual(EXPECTED_VALUE, itens.Count, $"É esperado que apenas os {EXPECTED_VALUE} itens predefinidos que tem BETA estejam na lista");

    }

    [TestMethod]
    public async Task ItShouldReturn4ItensFromFavorites() {
        const int EXPECTED_VALUE = 4;
        var itens = await mockRepository.GetAllFavorites();

        foreach (var item in itens)
            Assert.IsTrue(item.Favorito, "É Esperado que apenas repositórios favoritos estejam na lista!");

        Assert.AreEqual(EXPECTED_VALUE, itens.Count, $"É Esperado que apenas os {EXPECTED_VALUE} repositórios predefinidos marcados como favoritos estejam na lista!");

    }
}
