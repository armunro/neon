using System.CommandLine;
using System.CommandLine.Invocation;
using System.Drawing;
using Neon.Domain;
using Neon.Domain.Config;
using Serilog;

using Svg;

namespace Neon.Commands;

public class IconCommand : GenesisCommand
{
    private readonly GenesisConfigManager _config;
    private readonly ILogger _logger;
    private static Argument<string> ProjectNameArg = new("project", "The name of the project to generate icons for");

    public IconCommand(GenesisConfigManager config, ILogger logger) : base("icon",
        "Generate product icons in SVG,PNG and ICO formats")
    {
        _config = config;
        _logger = logger;
    }

    public override List<Argument> DefineArguments() => new() { ProjectNameArg };


    protected override Task<int> HandleAsync(InvocationContext context)
    {
        string projectArg = context.ParseResult.GetValueForArgument(ProjectNameArg).ToLower();
        if (projectArg == "all" || projectArg == "*")
            GenerateAllIcons();
        else
            GenerateSVGIcon(_config.GetProject(projectArg));
      
        return Task.FromResult(0);
    }

    private void GenerateSVGIcon(ProjectConfig project)
    {
        string destination = "icons_";
        SvgDocument doc = SvgDocument.Open("Templates/icon.svg");
        doc.GetElementById<SvgText>("Key").Text = FormatKey(project.Key);
        SvgGradientServer svgGradientServer = doc.GetElementById<SvgGradientServer>("_Linear1");
        string color1 = project.IconGradientColor1;
        string color2 = project.IconGradientColor2;
        if (string.IsNullOrWhiteSpace(color1)) color1 = "#9e0fff";
        if (string.IsNullOrWhiteSpace(color2)) color2 = "#42e3ff";
        
        svgGradientServer.Stops[0].StopColor = new SvgColourServer(IconHelpers.GetColorFromHex(color1));
        svgGradientServer.Stops[1].StopColor = new SvgColourServer(IconHelpers.GetColorFromHex(color2));

        foreach (int iconSize in _config.Config.Icon.Sizes)
        {
            Bitmap b = new Bitmap(iconSize, iconSize);
            Graphics g = Graphics.FromImage(b);
            doc.Draw(g, new SizeF(iconSize, iconSize));
            _logger.Information("Generating icon for {Project} at {Size}x{Size}", project.Key, iconSize);
            string savePath = Path.Join(destination,  $"{project.Key}_{iconSize}.png");
            Directory.CreateDirectory(destination);
            b.Save(savePath);
        }
    }

    private string FormatKey(string projectKey) =>
        $"{projectKey[0].ToString().ToUpper()}{projectKey[1].ToString().ToLower()}";

    private void GenerateAllIcons()
    {
        foreach (ProjectConfig project in _config.Config.Projects)
        {
            GenerateSVGIcon(project);
        }
    }
}