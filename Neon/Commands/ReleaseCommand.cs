using System.CommandLine.Invocation;
using Neon.Domain;

namespace Neon.Commands;

public class ReleaseCommand : GenesisCommand
{
    public ReleaseCommand() : base("release", "Create and manage releases") { }
    protected override Task<int> HandleAsync(InvocationContext context)
    {
        Console.WriteLine("Release command Not Implemented");
        return Task.FromResult(0);
    }
}