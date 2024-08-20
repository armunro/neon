using Serilog;
using YamlDotNet.Serialization;

namespace Neon.Domain.Config;

public class GenesisConfigManager
{
    private readonly ILogger _logger;
    GenesisConfig _config = new();

    public GenesisConfig Config => _config;
    
    public GenesisConfigManager(ILogger logger)
    {
        _logger = logger;
        LoadConfig();
    }

    private string GetConfigPath() => "neon.yaml";
    public void SaveConfig()
    {
        string yaml = new SerializerBuilder().WithIndentedSequences().Build().Serialize(_config);
        string configPath = GetConfigPath();
        _logger.Debug("Config Path: {ConfigPath}", configPath);
        File.WriteAllText(configPath, yaml);
        _logger.Information("Saved: {ConfigPath}", configPath);
    }

    public void LoadConfig()
    {
        string configPath = GetConfigPath();
        _logger.Debug("Config Path: {ConfigPath}", configPath);
        string yaml = File.ReadAllText(configPath);
        _config = new DeserializerBuilder().Build().Deserialize<GenesisConfig>(yaml);
    }
    
    public ProjectConfig GetProject(string projectName)
    {
        return _config.Projects.First(p => p.Key == projectName);
    }
}