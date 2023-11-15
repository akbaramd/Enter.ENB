using Enter.ENB.Example.DbMigrator;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

await builder.AddApplicationAsync<EnterEnbExampleDbMigratorModule>();


var app = builder.Build();

await app.InitializeApplicationAsync();



