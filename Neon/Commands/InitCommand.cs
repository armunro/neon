using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using Neon.Domain;
using Neon.Domain.Config;

namespace Neon.Commands;

public class InitCommand : NeonCommand
{
    private readonly NeonConfigManager _config;

    private static readonly Argument<string> ProjectNameArgument = new("name", "The name of the project");
    private static readonly Argument<string> ProjectKeyArgument = new("key", "The key of the project");
    public override List<Argument> DefineArguments() => new() {ProjectKeyArgument,  ProjectNameArgument };
    
    public InitCommand(NeonConfigManager config) : base("init", "Start a new project")
    {
        _config = config;
    }
    protected override Task<int> HandleAsync(InvocationContext context)
    {
        string key = context.ParseResult.GetValueForArgument(ProjectKeyArgument);
        string name = context.ParseResult.GetValueForArgument(ProjectNameArgument);
        
        _config.Config.Projects.Add(new ProjectConfig(key, name));
        _config.SaveConfig();
        Process process = Process.Start("gh", $"repo create {name} --add-readme --private" );
        process.WaitForExit();
        return Task.FromResult(0);
        
    }
}   