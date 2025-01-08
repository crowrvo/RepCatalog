namespace Persistence.InMemory.Entities;

public class Repositorio {

    //EF CORE CONSTRUCTOR
    protected Repositorio() { }

    public Repositorio(string nome, string descricao, string linguagem, string donoDoRepositorio, DateTime? ultimaAtualizacao) {
        Nome = nome;
        Descricao = descricao;
        Linguagem = linguagem;
        DonoDoRepositorio = donoDoRepositorio;
        UltimaAtualizacao = ultimaAtualizacao;
    }

    public int? Id { get; set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Linguagem { get; private set; }
    public string DonoDoRepositorio { get; private set; }
    public DateTime? UltimaAtualizacao { get; private set; }
    public bool Favorito { get; private set; } = false;

    public void Update(string nome, string descricao, string linguagem, string donoDoRepositorio, DateTime? ultimaAtualizacao) {
        Nome = nome;
        Descricao = descricao;
        Linguagem = linguagem;
        DonoDoRepositorio = donoDoRepositorio;
        UltimaAtualizacao = ultimaAtualizacao;
    }

    public void DefinirFavorito() {
        Favorito = true;
    }

    public void RemoverFavorito() {
        Favorito = false;
    }

}
