using Microsoft.Extensions.Hosting;

using RotaViagem.AppConsole.Extensions;

namespace RotaViagem.AppConsole.Tests;

public class IntegrationTestBase : IDisposable
{
    protected readonly IHost Host;
    protected readonly IServiceProvider ServiceProvider;

    public IntegrationTestBase()
    {
        var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder();
        builder.Services.AddApplicationServices();
        Host = builder.Build();
        ServiceProvider = Host.Services;
    }

    public void Dispose()
    {
        Host.Dispose();
    }
}