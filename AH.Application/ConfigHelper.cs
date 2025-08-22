using Microsoft.Extensions.Configuration;

public static class ConfigHelper
{
    private static IConfiguration? _config;

    public static void Initialize(IConfiguration configuration)
    {
        _config = configuration;
    }

    public static string GetConnectionString()
    {
        if (_config is null)
            throw new InvalidOperationException("ConfigHelper not initialized");

        return _config.GetConnectionString("DefaultConnection")??"";
    }
}
