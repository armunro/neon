using System.CommandLine.Invocation;
using Cosmic.CommandLine;
using Cosmic.CommandLine.Attributes;
using Neon.Domain;
using Neon.Domain.Config;

namespace Neon.Commands;

[CliCommand("config", "Gets/set config values")]
public class ConfigCommand : CliCommand
{
    private readonly NeonConfigManager _config;
    public ConfigCommand(NeonConfigManager config)
    {
        _config = config;
    }



    protected override Task<int> ExecuteCommand(CliCommandContext context)
    {
        _config.SaveConfig();
        return Task.FromResult(0);
    }
}