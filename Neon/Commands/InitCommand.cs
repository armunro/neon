using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using Cosmic.CommandLine;
using Cosmic.CommandLine.Attributes;
using Neon.Domain;
using Neon.Domain.Config;

namespace Neon.Commands;

[CliCommand("init", "Start a new project")]
public class InitCommand : CliCommand
{
    private readonly NeonConfigManager _config;

    private static readonly Argument<string> ProjectNameArgument = new("name", "The name of the project");
    private static readonly Argument<string> ProjectKeyArgument = new("key", "The key of the project");
    public List<Argument> DefineArguments() => new() { ProjectKeyArgument, ProjectNameArgument };

    public InitCommand(NeonConfigManager config)
    {
        _config = config;
    }

    protected override Task<int> ExecuteCommand(CliCommandContext context)
    {
        string key = context.Argument<string>(ProjectKeyArgument);
        string name = context.Argument<string>(ProjectNameArgument);

        _config.Config.Projects.Add(new ProjectConfig(key, name));
        _config.SaveConfig();
        Process process = Process.Start("gh", $"repo create {name} --add-readme --private");
        process.WaitForExit();
        return Task.FromResult(0);
    }
}