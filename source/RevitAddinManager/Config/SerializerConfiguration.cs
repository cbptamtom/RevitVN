using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace RevitAddinManager.Config;

public static class SerializerConfiguration
{
    public static void AddSerializerOptions(this IServiceCollection services)
    {
        services.Configure<JsonSerializerOptions>(options =>
        {
            options.WriteIndented = true;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.Converters.Add(new JsonStringEnumConverter());
        });
    }
}