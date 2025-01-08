using System.ComponentModel.DataAnnotations;
using Flunt.Validations;
using RepCatalog.Abstractions;
using RepCatalog.Commands;

namespace RepCatalog.Contracts;

public class CreateRepositoryContract : Contract<CreateRepositoryCommand> {

    public CreateRepositoryContract(CreateRepositoryCommand createCommand) {
        Requires()
           .IsGreaterThan(createCommand.Nome, 4, "Nome", "O campo Nome precisa ter no minimo 4 caracteres!")
           .IsLowerThan(createCommand.Descricao, 100, "Descricao", "O campo Descrição só pode ter no máximo 100 caracteres!")
           .IsNotNullOrEmpty(createCommand.Nome, "Nome", "O Campo Nome é obrigatório!")
           .IsNotNullOrEmpty(createCommand.Descricao, "Descricao", "O Campo Descricao é obrigatório!")
           .IsNotNullOrEmpty(createCommand.Linguagem, "Linguagem", "O Campo Linguagem é obrigatório!")
           .IsNotNullOrEmpty(createCommand.DonoDoRepositorio, "DonoDoRepositorio", "O Campo DonoDoRepositorio é obrigatório!")
           .IsNotNullOrEmpty(createCommand.UltimaAtualizacao, "UltimaAtualizacao", "O Campo UltimaAtualizacao é obrigatório!");


        if (!DateTime.TryParse(createCommand.UltimaAtualizacao, out _)) {
            AddNotification("UltimaAtualizacao", "Informe uma data válida!");
        }
    }
};
