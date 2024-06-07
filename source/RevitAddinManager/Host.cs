using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Module1.ViewModels;
using Module1.Views;
using Module2.ViewModels;
using Module2.Views;
using Module3.ViewModels;
using Module3.Views;
using RevitAddinManager.Config;

namespace RevitAddinManager;

/// <summary>
///     Provides a host for the application's services and manages their lifetimes
/// </summary>
public static class Host
{
    // private static IServiceProvider _serviceProvider;
    private static IHost _host;

    /// <summary>
    ///     Starts the host and configures the application's services
    /// </summary>
    public static void Start()
    {
        var builder = new HostApplicationBuilder(new HostApplicationBuilderSettings
        {
            ContentRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly()!.Location),
            DisableDefaults = true
        });
        
        //Logging
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilogConfiguration();
        
        
        // Configuration
        builder.Services.AddSerializerOptions();
        
        // Services
        builder.Services.AddTransient<Module1.Commands.ShowWindowComponent>();
        builder.Services.AddTransient<Module2.Commands.ShowWindowComponent>();
        builder.Services.AddTransient<Module3.Commands.ShowWindowComponent>(); 
        
        //Module1
        builder.Services.AddTransient<Module1View>();
        builder.Services.AddTransient<Module1ViewModel>();
        //Module2
        builder.Services.AddTransient<Module2View>();
        builder.Services.AddTransient<Module2ViewModel>();

        //Module3
        builder.Services.AddTransient<Module3View>();
        builder.Services.AddTransient<Module3ViewModel>();


        _host = builder.Build();
        _host.Start();
    }
    
    /// <summary>
    ///     Stops the host
    /// </summary>
    public static void Stop()
    {
        _host.StopAsync();
    }

public static T GetService<T>() where T : class
    {
        return _host.Services.GetService(typeof(T)) as T;
    }
}