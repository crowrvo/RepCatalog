using Persistence.InMemory.Entities;

namespace SystemTests;
[TestClass]
public class RepositorioTests {

    [TestMethod]
    public void ItShouldChangeTheRepositorioFavoriteStateToTrue() {
        var stu = new Repositorio("Repositorio de Teste 1", "Repositorio feito pelo mock", "C#", "Sistema", DateTime.Now);

        stu.DefinirFavorito();
        Assert.IsTrue(stu.Favorito);
    }

    [TestMethod]
    public void ItShouldChangeTheRepositorioFavoriteStateToFalse() {
        var stu = new Repositorio("Repositorio de Teste 1", "Repositorio feito pelo mock", "C#", "Sistema", DateTime.Now);

        stu.RemoverFavorito();
        Assert.IsFalse(stu.Favorito);
    }

    [TestMethod]
    public void ItShouldChangeTheRepositorioName() {
        var stu = new Repositorio("Repositorio de Teste 1", "Repositorio feito pelo mock", "C#", "Sistema",  DateTime.Now);

        stu.Update(
            "NOVO NOME",
            stu.Descricao,
            stu.Linguagem,
            stu.DonoDoRepositorio,
            stu.UltimaAtualizacao
            );

        Assert.AreEqual(stu.Nome, "NOVO NOME");
    }
}
