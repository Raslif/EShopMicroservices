using Carter;
using Catalog.API.DataAccess.Abstracts;
using Catalog.API.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductDocumentRepo, ProductDocumentRepo>();

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
