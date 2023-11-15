using Enter.ENB.Domain.Repository;
using Enter.ENB.Example.Api;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

await builder.AddApplicationAsync<EnterEnbExampleApiModule>();

var app = builder.Build();

await app.InitializeApplicationAsync();

var service =  await app.Services.CreateScope().ServiceProvider.GetRequiredService<IUserAppService>().GetByUsernameAsync("akbar");
var genericRepository = await app.Services.CreateScope().ServiceProvider.GetRequiredService<IRepository<EntUser,Guid>>().FindAsync(x=>x.UserName == "akbar");
var repository =await app.Services.CreateScope().ServiceProvider.GetRequiredService<IEntUserRepository>().FindAsync(x=>x.UserName == "akbar");;

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