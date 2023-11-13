using Enter.ENB.Example.Api;
using Enter.ENB.Identity.Application.Contracts.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

await builder.AddApplicationAsync<EnterEnbExampleApiModule>();

var app = builder.Build();

await app.InitializeApplicationAsync();


var userService = app.Services.CreateScope().ServiceProvider.GetRequiredService<IUserAppService>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();