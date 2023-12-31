﻿using System.Reflection;
using Enter.ENB;
using Enter.ENB.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modularity;
using Shouldly;

public class EntApplication_Initialize_Tests
{
    
    [Fact]
    public async Task Should_Initialize_Single_Module_Async()
    {
        using var application = await EntApplicationFactory.CreateAsync<IndependentEmptyModule>();
        //Assert
        var module = application.Services.GetSingletonInstance<IndependentEmptyModule>();

        module.PreConfigureServicesAsyncIsCalled.ShouldBeTrue();
        module.PreConfigureServicesIsCalled.ShouldBeTrue();

        module.ConfigureServicesAsyncIsCalled.ShouldBeTrue();
        module.ConfigureServicesIsCalled.ShouldBeTrue();

        module.PostConfigureServicesAsyncIsCalled.ShouldBeTrue();
        module.PostConfigureServicesIsCalled.ShouldBeTrue();

        //Act
        await application.InitializeAsync();

        //Assert
        application.ServiceProvider.GetRequiredService<IndependentEmptyModule>().ShouldBeSameAs(module);
        module.OnApplicationInitializeAsyncIsCalled.ShouldBeTrue();
        module.OnApplicationInitializeIsCalled.ShouldBeTrue();
        //Act
        await application.ShutdownAsync();

        //Assert
        module.OnApplicationShutdownAsyncIsCalled.ShouldBeTrue();
        module.OnApplicationShutdownIsCalled.ShouldBeTrue();
    }

    [Fact]
    public void Should_Initialize_Single_Module()
    {
        using var application = EntApplicationFactory.Create<IndependentEmptyModule>();
        
        //Assert
        var module = application.Services.GetSingletonInstance<IndependentEmptyModule>();
        module.PreConfigureServicesIsCalled.ShouldBeTrue();
        module.ConfigureServicesIsCalled.ShouldBeTrue();
        module.PostConfigureServicesIsCalled.ShouldBeTrue();

        //Act
        application.Initialize();

        //Assert
        application.ServiceProvider.GetRequiredService<IndependentEmptyModule>().ShouldBeSameAs(module);
        module.OnApplicationInitializeIsCalled.ShouldBeTrue();

        //Act
        application.Shutdown();

        //Assert
        module.OnApplicationShutdownIsCalled.ShouldBeTrue();
    }

    // [Fact]
    // public async Task Should_Initialize_PlugIn_Async()
    // {
    //     using (var application = await EntApplicationFactory.CreateAsync<IndependentEmptyModule>(options =>
    //            {
    //                options.PlugInSources.AddTypes(typeof(IndependentEmptyPlugInModule));
    //            }))
    //     {
    //         //Assert
    //         var plugInModule = application.Services.GetSingletonInstance<IndependentEmptyPlugInModule>();
    //
    //         plugInModule.PreConfigureServicesAsyncIsCalled.ShouldBeTrue();
    //         plugInModule.PreConfigureServicesIsCalled.ShouldBeTrue();
    //
    //         plugInModule.ConfigureServicesAsyncIsCalled.ShouldBeTrue();
    //         plugInModule.ConfigureServicesIsCalled.ShouldBeTrue();
    //
    //         plugInModule.PostConfigureServicesAsyncIsCalled.ShouldBeTrue();
    //         plugInModule.PostConfigureServicesIsCalled.ShouldBeTrue();
    //
    //         //Act
    //         await application.InitializeAsync();
    //
    //         //Assert
    //         application.ServiceProvider.GetRequiredService<IndependentEmptyPlugInModule>().ShouldBeSameAs(plugInModule);
    //
    //         plugInModule.OnPreApplicationInitializationAsyncIsCalled.ShouldBeTrue();
    //         plugInModule.OnPreApplicationInitializationIsCalled.ShouldBeTrue();
    //
    //         plugInModule.OnApplicationInitializeAsyncIsCalled.ShouldBeTrue();
    //         plugInModule.OnApplicationInitializeIsCalled.ShouldBeTrue();
    //
    //         plugInModule.OnPostApplicationInitializationAsyncIsCalled.ShouldBeTrue();
    //         plugInModule.OnPostApplicationInitializationIsCalled.ShouldBeTrue();
    //
    //         //Act
    //         await application.ShutdownAsync();
    //
    //         //Assert
    //         plugInModule.OnApplicationShutdownAsyncIsCalled.ShouldBeTrue();
    //         plugInModule.OnApplicationShutdownIsCalled.ShouldBeTrue();
    //     }
    // }
    //
    // [Fact]
    // public void Should_Initialize_PlugIn()
    // {
    //     using (var application = EntApplicationFactory.Create<IndependentEmptyModule>(options =>
    //     {
    //         options.PlugInSources.AddTypes(typeof(IndependentEmptyPlugInModule));
    //     }))
    //     {
    //         //Assert
    //         var plugInModule = application.Services.GetSingletonInstance<IndependentEmptyPlugInModule>();
    //         plugInModule.PreConfigureServicesIsCalled.ShouldBeTrue();
    //         plugInModule.ConfigureServicesIsCalled.ShouldBeTrue();
    //         plugInModule.PostConfigureServicesIsCalled.ShouldBeTrue();
    //
    //         //Act
    //         application.Initialize();
    //
    //         //Assert
    //         application.ServiceProvider.GetRequiredService<IndependentEmptyPlugInModule>().ShouldBeSameAs(plugInModule);
    //         plugInModule.OnPreApplicationInitializationIsCalled.ShouldBeTrue();
    //         plugInModule.OnApplicationInitializeIsCalled.ShouldBeTrue();
    //         plugInModule.OnPostApplicationInitializationIsCalled.ShouldBeTrue();
    //
    //         //Act
    //         application.Shutdown();
    //
    //         //Assert
    //         plugInModule.OnApplicationShutdownIsCalled.ShouldBeTrue();
    //     }
    // }

    [Fact]
    public void Should_Set_And_Get_ApplicationName_And_InstanceId()
    {
        var applicationName = "MyApplication";

        using (var application = EntApplicationFactory.Create<IndependentEmptyModule>(options =>
               {
                   options.ApplicationName = applicationName;
               }))
        {
            application.ApplicationName.ShouldBe(applicationName);
            application.Services.GetApplicationName().ShouldBe(applicationName);

            application.Initialize();

            var appInfo = application.ServiceProvider.GetRequiredService<IApplicationInfoAccessor>();
            appInfo.ApplicationName.ShouldBe(applicationName);
            appInfo.InstanceId.ShouldNotBeNullOrEmpty();
        }

        using (var application = EntApplicationFactory.Create<IndependentEmptyModule>(options =>
               {
                   options.Services.ReplaceConfiguration(new ConfigurationBuilder()
                       .AddInMemoryCollection(new Dictionary<string, string> {{"ApplicationName", applicationName}})
                       .Build());
               }))
        {

            application.ApplicationName.ShouldBe(applicationName);
            application.Services.GetApplicationName().ShouldBe(applicationName);

            application.Initialize();

            application.ServiceProvider
                .GetRequiredService<IApplicationInfoAccessor>()
                .ApplicationName
                .ShouldBe(applicationName);
        }

        applicationName = Assembly.GetEntryAssembly()?.GetName().Name;
        using (var application = EntApplicationFactory.Create<IndependentEmptyModule>())
        {
            application.ApplicationName.ShouldBe(applicationName);
            application.Services.GetApplicationName().ShouldBe(applicationName);

            application.Initialize();

            application.ServiceProvider
                .GetRequiredService<IApplicationInfoAccessor>()
                .ApplicationName
                .ShouldBe(applicationName);
        }
    }

    [Fact]
    public void Should_Set_And_Get_Environment()
    {
        // Default environment is Production
        using (var application = EntApplicationFactory.Create<IndependentEmptyModule>())
        {
            var abpHostEnvironment = application.Services.GetSingletonInstance<IEntHostEnvironment>();
            abpHostEnvironment.EnvironmentName.ShouldBe(Environments.Production);

            application.Initialize();

            abpHostEnvironment = application.ServiceProvider.GetRequiredService<IEntHostEnvironment>();
            abpHostEnvironment.EnvironmentName.ShouldBe(Environments.Production);
        }

        // Set environment
        using (var application = EntApplicationFactory.Create<IndependentEmptyModule>(options =>
               {
                   options.Environment = Environments.Staging;
               }))
        {
            var abpHostEnvironment = application.Services.GetSingletonInstance<IEntHostEnvironment>();
            abpHostEnvironment.EnvironmentName.ShouldBe(Environments.Staging);

            application.Initialize();

            abpHostEnvironment = application.ServiceProvider.GetRequiredService<IEntHostEnvironment>();
            abpHostEnvironment.EnvironmentName.ShouldBe(Environments.Staging);
        }
    }

    [Fact]
    public async Task Should_Resolve_Root_Service_Provider()
    {
        using (var application = await EntApplicationFactory.CreateAsync<IndependentEmptyModule>())
        {
            await application.InitializeAsync();

            application
                .ServiceProvider
                .GetRequiredService<IRootServiceProvider>()
                .ShouldNotBeNull();
        }
    }
}