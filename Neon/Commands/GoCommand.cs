using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using Cosmic.CommandLine;
using Cosmic.CommandLine.Attributes;
using Neon.Domain.Config;


namespace Neon.Commands;

[CliCommand("go", "Quick launchers for project resources.")]
public class GoCommand : CliCommand
{
    private readonly NeonConfigManager _config;
    public static Argument<string> ResourceArgument = new("resource", "The resource to go to.");
    public static Argument<string> ProjectArgument = new("project", "The project name.");

    public GoCommand(NeonConfigManager config) 
    {
        _config = config;
    }
    
    public  List<Argument> DefineArguments() => new() { ResourceArgument, ProjectArgument };


    protected override Task<int> ExecuteCommand(CliCommandContext context)
    {
        string resourceArg = context.Argument<string>(ResourceArgument);
        string projectArg =context.Argument<string>(ProjectArgument);
        ProjectConfig project = _config.GetProjectByKey(projectArg);
        switch (resourceArg)
        {
            case "repo":
                Process.Start("gh", $"repo view {project.Repo} --web");
                break;
            case "files":
                Process.Start("explorer", project.WorkingCopy);
                break;
            case "dev":
                Process.Start(_config.Config.IdePath, Path.Combine(project.WorkingCopy, $"{project.Name}.sln"));
                break;
        }

        return Task.FromResult(0);
    }
}