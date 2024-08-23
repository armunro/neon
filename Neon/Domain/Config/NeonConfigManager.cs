using Serilog;
using YamlDotNet.Serialization;

namespace Neon.Domain.Config;

public class NeonConfigManager
{
    private readonly ILogger _logger;
    RootConfig _config = new();

    public RootConfig Config => _config;
    
    public NeonConfigManager(ILogger logger)
    {
        _logger = logger;
        LoadConfig();
    }

    private string GetConfigPath() => "neon.yaml";
    public void SaveConfig()
    {
        string yaml = new SerializerBuilder().WithIndentedSequences().Build().Serialize(_config);
        string configPath = GetConfigPath();
        _logger.Debug("Save Config Path: {ConfigPath}", configPath);
        File.WriteAllText(configPath, yaml);
        _logger.Information("Saved: {ConfigPath}", configPath);
    }

    public void LoadConfig()
    {
        string configPath = GetConfigPath();
        _logger.Debug("Load Config Path: {ConfigPath}", configPath);
        string yaml = File.ReadAllText(configPath);
        _config = new DeserializerBuilder().Build().Deserialize<RootConfig>(yaml);
    }
    
    public ProjectConfig GetProjectByKey(string projectKey)
    {
        return _config.Projects.First(p => p.Key == projectKey);
    }
}