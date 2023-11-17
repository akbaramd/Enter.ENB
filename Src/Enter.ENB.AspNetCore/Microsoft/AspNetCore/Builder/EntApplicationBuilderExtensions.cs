using System.Threading.Tasks;
using Enter.ENB;
using Enter.ENB.Statics;
using Enter.ENB.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microsoft.AspNetCore.Builder;

public static class EntApplicationBuilderExtensions
{
    private const string ExceptionHandlingMiddlewareMarker = "_EntExceptionHandlingMiddleware_Added";

    public async static Task InitializeApplicationAsync( this IApplicationBuilder app)
    {
        EntCheck.NotNull(app, nameof(app));

        var application = app.ApplicationServices.GetRequiredService<IEntApplicationWithExternalServiceProvider>();
        var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        applicationLifetime.ApplicationStopping.Register(() =>
        {
            AsyncHelper.RunSync(() => application.ShutdownAsync());
        });

        applicationLifetime.ApplicationStopped.Register(() =>
        {
            application.Dispose();
        });

        await application.InitializeAsync(app.ApplicationServices);
    }

    public static void InitializeApplication(this IApplicationBuilder app)
    {
        EntCheck.NotNull(app, nameof(app));

        // app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value = app;
        var application = app.ApplicationServices.GetRequiredService<IEntApplicationWithExternalServiceProvider>();
        var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        applicationLifetime.ApplicationStopping.Register(() =>
        {
            application.Shutdown();
        });

        applicationLifetime.ApplicationStopped.Register(() =>
        {
            application.Dispose();
        });

        application.Initialize(app.ApplicationServices);
    }

   
}