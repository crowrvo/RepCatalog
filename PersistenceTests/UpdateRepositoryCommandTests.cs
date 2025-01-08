using RepCatalog.Commands;

namespace SystemTests;
[TestClass]
public class UpdateRepositoryCommandTests {

    [TestMethod]
    public void ItShouldThrowValidationErrorByMissingRequiredFields() {
        UpdateRepositoryCommand stu = new(
            nome: "",
            descricao: "",
            donoDoRepositorio: "",
            linguagem: "",
            ultimaAtualizacao: ""
        );

        Assert.IsFalse(stu.IsValid, "É esperado que o validador marque o DTO como inválido");

        // Só uma segunda checagem, não é extremamente necessária!
        Assert.AreEqual(7, stu.Notifications.Count, "É esperado que o validador informe que os 7 erros de campos requeridos ou mal formatados estejam inválidos");

    }

    [TestMethod]
    public void ItShouldNotThrowAnyValidationError() {
        UpdateRepositoryCommand stu = new(
            nome: "Repo de teste",
            descricao: "Apenas um Teste",
            donoDoRepositorio: "João Felipe",
            linguagem: "C#",
            ultimaAtualizacao: "2025-01-07"
        );

        Assert.IsTrue(stu.IsValid, "É esperado que o validado marque o DTO como válido");
    }

    [TestMethod]
    public void ItShouldThrowAValidationErrorByNomeHaveLessThan5Characters() {
        UpdateRepositoryCommand stu = new(
            nome: "Repo",
            descricao: "Apenas um Teste",
            donoDoRepositorio: "João Felipe",
            linguagem: "C#",
            ultimaAtualizacao: "2025-01-07"
        );

        Assert.IsFalse(stu.IsValid, "É esperado que o validador marque o DTO como inválido");
        Assert.AreEqual(1, stu.Notifications.Count, "É esperado que o validador informe 1 erro referente ao campo nome que está inválido");
    }


    [TestMethod]
    public void ItShouldThrowAValidationErrorByDTOHasInvalidDateTime() {
        UpdateRepositoryCommand stu = new(
            nome: "Repo de Teste",
            descricao: "Apenas um Teste",
            donoDoRepositorio: "João Felipe",
            linguagem: "C#",
            ultimaAtualizacao: "2025-13-01"
        );

        Assert.IsFalse(stu.IsValid, "É esperado que o validado marque o DTO como inválido");

        Assert.AreEqual(1, stu.Notifications.Count, "É esperado que o validador informe 1 erro referente ao campo data está inválido");

    }
}
