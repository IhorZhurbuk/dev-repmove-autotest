using Microsoft.Extensions.Configuration;

namespace dev_repmove_autotest.Utils.ConfigManager;

public class ConfigurationManager
{
    private static ConfigurationManager? _instance;
    private readonly IConfiguration _configuration;

    private ConfigurationManager()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsetings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static ConfigurationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ConfigurationManager();
            }
            return _instance;
        }
    }

    public string BaseUrl => _configuration["BaseUrl"] ?? throw new InvalidOperationException("BaseUrl is not configured");

    public UserConfig User => _configuration.GetSection("User").Get<UserConfig>() ?? throw new InvalidOperationException("User configuration is missing");
}