using Flunt.Notifications;
using RepCatalog.Contracts;

namespace RepCatalog.Commands;

public class CreateRepositoryCommand : Notifiable<Notification> {
    public CreateRepositoryCommand(string nome, string descricao, string linguagem, string ultimaAtualizacao, string donoDoRepositorio) {
        Nome = nome;
        Descricao = descricao;
        Linguagem = linguagem;
        UltimaAtualizacao = ultimaAtualizacao;
        DonoDoRepositorio = donoDoRepositorio;
        AddNotifications(new CreateRepositoryContract(this));
    }

    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Linguagem { get; set; }
    public string UltimaAtualizacao { get; set; }
    public string DonoDoRepositorio { get; set; }

};
