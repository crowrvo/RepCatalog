using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Persistence.InMemory;
using RepCatalog.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Eu gosto desse estilo de adi��o de depencia pois dessa forma n�o preciso ficar instalando EF Core e outras depend�ncias 
// em todos os projetos em que essa biblioteca for ser adicionada, ela fica isolada e exclusiva em seu m�dulo, e nos permite
// uma manuten��o limpa, facil de ler e alterar, organizada e livre de duplica��o de c�digo.
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
