using System.CommandLine;
using Autofac;
using Cosmic.Aspects.Logs;
using Cosmic.CommandLine;
using Cosmic.CommandLine.Extensions;
using Neon.Commands;
using Neon.Domain.Config;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

CliApp app = new CliApp();

app.RegisterDependencies(builder =>
{
    builder.RegisterCosmicCommands("[Ne]on - A simple command line tool for devops.");
    builder.RegisterCosmicLogging();
    
    builder.RegisterType<ConfigCommand>().AsSelf();
    builder.RegisterType<IconCommand>().AsSelf();
    builder.RegisterType<InitCommand>().AsSelf();
    builder.RegisterType<GoCommand>().AsSelf();
    builder.RegisterType<NeonConfigManager>().AsSelf().SingleInstance();
    builder.RegisterType<RootCommand>().SingleInstance().AsSelf();
    builder.RegisterType<NeonConfigManager>().AsSelf().SingleInstance();
});








app.StartsWith(container =>
{
    RootCommand rootCommand = container.Resolve<RootCommand>();
    Command icon = container.Resolve<IconCommand>();
    Command config = container.Resolve<ConfigCommand>();
    Command init = container.Resolve<InitCommand>();
    Command go = container.Resolve<GoCommand>();
    rootCommand.AddCommand(icon);
    rootCommand.AddCommand(init);
    rootCommand.AddCommand(config);
    rootCommand.AddCommand(go);
    rootCommand.InvokeAsync(args).Wait();
}).Build();
app.Start();