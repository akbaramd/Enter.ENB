using Enter.ENB.Example.DbMigrator;

var builder = WebApplication.CreateBuilder(args);

await builder.AddApplicationAsync<EnterEnbExampleDbMigratorModule>();


var app = builder.Build();

await app.InitializeApplicationAsync();



