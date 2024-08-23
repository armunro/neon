using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using Neon.Domain;
using Neon.Domain.Config;

namespace Neon.Commands;

public class GoCommand : NeonCommand
{
    private readonly NeonConfigManager _config;
    public static Argument<string> ResourceArgument = new("resource", "The resource to go to.");
    public static Argument<string> ProjectArgument = new("project", "The project name.");

    public GoCommand(NeonConfigManager config) : base("go", "Quick launchers for project resources.")
    {
        _config = config;
    }
    
    public override List<Argument> DefineArguments() => new() { ResourceArgument, ProjectArgument };

    protected override Task<int> HandleAsync(InvocationContext context)
    {
        string resourceArg = context.ParseResult.GetValueForArgument(ResourceArgument);
        string projectArg = context.ParseResult.GetValueForArgument(ProjectArgument);
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