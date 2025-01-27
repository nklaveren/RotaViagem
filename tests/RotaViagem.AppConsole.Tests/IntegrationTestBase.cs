using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

using RotaViagem.AppConsole.Extensions;
using RotaViagem.AppConsole.Interfaces;

namespace RotaViagem.AppConsole.Tests;

public class IntegrationTestBase : IDisposable
{
    protected readonly IHost Host;
    protected readonly IServiceProvider ServiceProvider;

    public IntegrationTestBase()
    {
        var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder();
        builder.Services.AddApplicationServices();
        //remove console service
        builder.Services.Remove(builder.Services.First(s => s.ServiceType == typeof(IConsoleService)));
        builder.Services.AddSingleton<IConsoleService>(Mock.Of<IConsoleService>());
        Host = builder.Build();
        ServiceProvider = Host.Services;
    }

    public void Dispose()
    {
        Host.Dispose();
    }
}