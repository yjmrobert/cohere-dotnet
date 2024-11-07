using Microsoft.Extensions.Configuration;

namespace Cohere.IntegrationTests;

/// <summary>
/// Configuration settings for the integration tests
/// </summary>
public static class Configuration
{
    private static IConfiguration? _configuration;

    /// <summary>
    /// Initializes the configuration settings by loading from a JSON file
    /// </summary>
    public static void Initialize()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();
    }

    /// <summary>
    /// Gets the API key from the configuration file
    /// </summary>
    public static string ApiKey => _configuration?.GetValue<string>("Cohere:ApiKey") 
        ?? throw new InvalidOperationException("API key is not configured.");
}
