namespace RepCatalog.Abstractions;

public static class RepositorioErrors {
    public static readonly Error NotFound = new("Repository.NotFound", "Repositório não encontrado!");
    public static readonly Error BadRequest = new("Repository.BadRequest", "Um ou mais campos estão inválidos!");
    public static readonly Error NotAllowed = new("Repository.NotAllowed", "Não é possivel realizar essa operação para esse Repositório!");
    public static readonly Error NotAllowedDeleteFavorite = new(
        "Repository.NotAllowed",
        "Não é possivel realizar essa operação para esse Repositório.\n Você não pode deletar um repositório marcado como favorito!");

}
