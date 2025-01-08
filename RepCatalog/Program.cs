using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Persistence.InMemory;
using RepCatalog.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Eu gosto desse estilo de adição de depencia pois dessa forma não preciso ficar instalando EF Core e outras dependências 
// em todos os projetos em que essa biblioteca for ser adicionada, ela fica isolada e exclusiva em seu módulo, e nos permite
// uma manutenção limpa, facil de ler e alterar, organizada e livre de duplicação de código.
builder.Services.AddPersistenceInMemory();

builder.Services.AddControllers(options => {
    options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});


builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
