using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using Neon.Domain;

namespace Neon.Commands;

public class InitCommand : GenesisCommand
{
    public static Argument<string> ProjectNameArgument = new("project", "The name of the project");
    public override List<Argument> DefineArguments() => new() { ProjectNameArgument };
    
    public InitCommand() : base("init", "Start a new project") { }
    protected override Task<int> HandleAsync(InvocationContext context)
    {
        string project = context.ParseResult.GetValueForArgument(ProjectNameArgument);
        //execute system command
        Process process = Process.Start("gh", $"repo create {project} --add-readme --private" );
        process.WaitForExit();
        return Task.FromResult(0);
        
    }
}   