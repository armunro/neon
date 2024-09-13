using System.CommandLine;
using Autofac;
using Cosmic.Aspects.Logs;
using Cosmic.CommandLine;
using Cosmic.CommandLine.Extensions;
using Neon.Commands;
using Neon.Domain.Config;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

CliApp app = new();

app.RegisterDependencies(builder =>
{
    builder.RegisterCosmicCommands("[Ne]on - A simple command line tool for devops.");
    builder.RegisterCosmicLogging();
    builder.RegisterType<NeonConfigManager>().AsSelf().SingleInstance();
    builder.RegisterType<RootCommand>().SingleInstance().AsSelf();
    builder.RegisterType<NeonConfigManager>().AsSelf().SingleInstance();
});

app.AddConfigStep(app =>
{
    RootCommand rootCommand = app.Container.Resolve<RootCommand>();
    Command icon = app.Container.Resolve<IconCommand>();
    Command config = app.Container.Resolve<ConfigCommand>();
    Command init = app.Container.Resolve<InitCommand>();
    Command go = app.Container.Resolve<GoCommand>();
    rootCommand.AddCommand(icon);
    rootCommand.AddCommand(init);
    rootCommand.AddCommand(config);
    rootCommand.AddCommand(go);
    rootCommand.InvokeAsync(args).Wait();
}).Build();
app.Start();