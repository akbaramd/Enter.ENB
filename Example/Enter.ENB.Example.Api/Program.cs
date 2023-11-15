using Enter.ENB.Example.Api;
using Enter.ENB.Identity.Application.Contracts.Users;
using Enter.ENB.Identity.Application.Contracts.Users.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

await builder.AddApplicationAsync<EnterEnbExampleApiModule>();

var app = builder.Build();

await app.InitializeApplicationAsync();

var service = app.Services.CreateScope().ServiceProvider.GetRequiredService<IUserAppService>();

var res = await service.CreateAsync(new CreateUpdateUserDto()
{
UserName = "akbar",
Password = "ahmadi",
FirstName = "asdasd",
LastName = "sdads"
});

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