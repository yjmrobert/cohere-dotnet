using Microsoft.Extensions.Configuration;

namespace Cohere.IntegrationTests;

/// <summary>
/// Configuration settings for the integration tests
/// </summary>
public static class Configuration
{
    private static readonly IConfiguration _configuration;

    static Configuration()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static string ApiKey => _configuration.GetValue<string>("Cohere:ApiKey") 
        ?? throw new InvalidOperationException("API key is not configured.");
}