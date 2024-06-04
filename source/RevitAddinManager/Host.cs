using Microsoft.Extensions.DependencyInjection;

namespace RevitAddinManager;

/// <summary>
///     Provides a host for the application's services and manages their lifetimes
/// </summary>
public static class Host
{
    private static IServiceProvider _serviceProvider;

    /// <summary>
    ///     Starts the host and configures the application's services
    /// </summary>
    public static void Start()
    {
        var services = new ServiceCollection();

        _serviceProvider = services.BuildServiceProvider();
    }

    /// <summary>
    ///     Gets a service of the specified type
    /// </summary>
    /// <typeparam name="T">The type of service object to get</typeparam>
    /// <returns>A service object of type T or null if there is no such service</returns>
    public static T GetService<T>() where T : class
    {
        return _serviceProvider.GetService(typeof(T)) as T;
    }
}