using Microsoft.Extensions.Configuration;

namespace Helper.Configuration;
public static class ConfigurationHelper
{
    public static IConfiguration ReadConfiguration(string path)
    {
        var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
        var configPath = Path.Combine(projectRoot, $"src/Configuration/{path}");
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(configPath)
            .Build();

        return config;
    }

    public static string GetConfigurationValue(IConfiguration config, string key)
    {
        var value = config[key];
        if (!string.IsNullOrEmpty(value)) return value;
        var message = $"Attribute '{key}' has not been set in AppSettings.";
        throw new InvalidDataException(message);
    }
}