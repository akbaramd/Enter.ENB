using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Enter.ENB.Modularity;
using Enter.ENB.Test;

namespace Enter.ENB.DependencyInjection;

public class EntLazyServiceProvider_Tests
{
    [Fact]
    public void LazyServiceProvider_Should_Cache_Services()
    {
        using (var application = EntApplicationFactory.Create<TestModule>())
        {
            application.Initialize();

            var lazyServiceProvider = application.ServiceProvider.GetRequiredService<IEntLazyServiceProvider>();

            var transientTestService1 = lazyServiceProvider.LazyGetRequiredService<TransientTestService>();
            var transientTestService2 = lazyServiceProvider.LazyGetRequiredService<TransientTestService>();
            transientTestService1.ShouldBeSameAs(transientTestService2);

            var testCounter = application.ServiceProvider.GetRequiredService<ITestCounter>();
            testCounter.GetValue(nameof(TransientTestService)).ShouldBe(1);
        }
    }

    [DependsOnModules(typeof(EntTestModule))]
    private class TestModule : EntModule
    {
        public TestModule()
        {
            SkipAutoServiceRegistration = true;
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddType<TransientTestService>();
        }
    }

    private class TransientTestService : ITransientDependency
    {
        public TransientTestService(ITestCounter counter)
        {
            counter.Increment(nameof(TransientTestService));
        }
    }
}
