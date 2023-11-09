using EME.Module1;
using EME.Module2;
using Enter.Modularity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModularity(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapGet("/", () => $"Hello World! {nameof(Module2)}");

app.Run();