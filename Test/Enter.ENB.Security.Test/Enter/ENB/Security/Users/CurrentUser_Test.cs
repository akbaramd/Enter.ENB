using Enter.ENB.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Enter.ENB.Security.Users;


public class ICurrentUser_Test
{
    [Fact]
    public async Task Should_Initialize_Service()
    {
        using var application = await EntApplicationFactory.CreateAsync<IndependentEmptyModule>();

        await application.InitializeAsync();

        var currentUser = application.ServiceProvider.GetService<ICurrentUser>();

        currentUser.ShouldNotBeNull();
    }
}


[DependsOnModules(
    typeof(EntSecurityModule),
    typeof(EntTestModule)
    )]
public class IndependentEmptyModule : EntModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SkipAutoServiceRegistration = true;
        base.PreConfigureServices(context);
    }
  
}

