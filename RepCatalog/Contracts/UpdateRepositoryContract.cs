using Flunt.Validations;
using RepCatalog.Commands;

namespace RepCatalog.Contracts;

public class UpdateRepositoryContract : Contract<UpdateRepositoryCommand> {

    public UpdateRepositoryContract(UpdateRepositoryCommand updateCommand) {
        Requires()
             .IsGreaterThan(updateCommand.Nome, 4, "Nome", "O campo Nome precisa ter no minimo 4 caracteres!")
             .IsLowerThan(updateCommand.Descricao, 100, "Descricao", "O campo Descrição só pode ter no máximo 100 caracteres!")
             .IsNotNullOrEmpty(updateCommand.Nome, "Nome", "O Campo Nome é obrigatório!")
             .IsNotNullOrEmpty(updateCommand.Descricao, "Descricao", "O Campo Descricao é obrigatório!")
             .IsNotNullOrEmpty(updateCommand.Linguagem, "Linguagem", "O Campo Linguagem é obrigatório!")
             .IsNotNullOrEmpty(updateCommand.DonoDoRepositorio, "DonoDoRepositorio", "O Campo DonoDoRepositorio é obrigatório!")
             .IsNotNullOrEmpty(updateCommand.UltimaAtualizacao, "UltimaAtualizacao", "O Campo UltimaAtualizacao é obrigatório!");

        if (!DateTime.TryParse(updateCommand.UltimaAtualizacao, out _)) {
            AddNotification("UltimaAtualizacao", "Informe uma data válida!");
        }
    }
};
