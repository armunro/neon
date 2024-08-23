using System.CommandLine.Invocation;
using Neon.Domain;

namespace Neon.Commands;

public class ReleaseCommand : NeonCommand
{
    public ReleaseCommand() : base("release", "Create and manage releases") { }
    protected override Task<int> HandleAsync(InvocationContext context)
    {
        Console.WriteLine("Release command Not Implemented");
        //Build
        //Docker Build
        //pack/publish
        //Create Github release
        //Upload assets
        
        return Task.FromResult(0);
    }
}