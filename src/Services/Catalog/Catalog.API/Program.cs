using Catalog.API.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var product = new Product();

app.MapGet("/", () => "Hello World!");

app.Run();
