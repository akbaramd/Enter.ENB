using Enter.ENB;
using Enter.ENB.DependencyInjection;
using Enter.ENB.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace DependencyInjection;

public class AutoServiceRegistration_Tests
{
    [Fact]
    public async Task AutoServiceRegistration_Should_Not_Duplicate_Test_Async()
    {
        using (var application = await EntApplicationFactory.CreateAsync<TestModule>())
        {
            //Act
            await application.InitializeAsync();

            //Assert
            var services = application.ServiceProvider.GetServices<TestService>().ToList();
            services.Count.ShouldBe(1);
        }
    }

    [Fact]
    public void AutoServiceRegistration_Should_Not_Duplicate_Test()
    {
        using (var application = EntApplicationFactory.Create<TestModule>())
        {
            //Act
            application.Initialize();

            //Assert
            var services = application.ServiceProvider.GetServices<TestService>().ToList();
            services.Count.ShouldBe(1);
        }
    }
}

[DependsOnModules(typeof(TestDependedModule))]
public class TestModule : EntModule
{

}

public class TestDependedModule : EntModule
{

}

public class TestService : ITransientDependency
{

}
