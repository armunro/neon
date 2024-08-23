using System.CommandLine.Invocation;
using Neon.Domain;
using Neon.Domain.Config;

namespace Neon.Commands;

public class ConfigCommand : NeonCommand
{
    private readonly NeonConfigManager _config;
    public ConfigCommand(NeonConfigManager config) : base("config", "Gets/set config values")
    {
        _config = config;
    }

    protected override Task<int> HandleAsync(InvocationContext context)
    {
        _config.SaveConfig();
        return Task.FromResult(0);
    }
}