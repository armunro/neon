using System.CommandLine;
using Autofac;
using Neon.Commands;
using Neon.Domain.Config;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

ILogger Logger(IComponentContext componentContext) =>
    new LoggerConfiguration().MinimumLevel.Debug()
        .WriteTo.File(Path.Combine("logs", "log-.txt"),
            rollingInterval: RollingInterval.Day)
        .WriteTo.Console(theme: AnsiConsoleTheme.Code).CreateLogger();
ContainerBuilder builder = new();
builder.RegisterType<ConfigCommand>().AsSelf();
builder.RegisterType<IconCommand>().AsSelf();
builder.RegisterType<InitCommand>().AsSelf();
builder.RegisterType<GoCommand>().AsSelf();
builder.RegisterType<NeonConfigManager>().AsSelf().SingleInstance();
builder.RegisterType<RootCommand>().SingleInstance().AsSelf();
builder.Register<ILogger>((c, p) => Logger(c)).SingleInstance();

IContainer container = builder.Build();

RootCommand rootCommand = container.Resolve<RootCommand>();
Command icon = container.Resolve<IconCommand>();
Command config = container.Resolve<ConfigCommand>();
Command init = container.Resolve<InitCommand>();
Command go = container.Resolve<GoCommand>();
rootCommand.AddCommand(icon);
rootCommand.AddCommand(init);
rootCommand.AddCommand(config);
rootCommand.AddCommand(go);


await rootCommand.InvokeAsync(args);


    