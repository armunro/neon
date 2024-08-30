using System.CommandLine;
using System.CommandLine.Invocation;
using System.Drawing;
using Cosmic.CommandLine;
using Cosmic.CommandLine.Attributes;
using Neon.Domain;
using Neon.Domain.Config;
using Serilog;
using Svg;

namespace Neon.Commands;

[CliCommand("icon", "Generate product icons in SVG,PNG and ICO formats")]
public class IconCommand : CliCommand
{
    private readonly NeonConfigManager _config;
    private readonly ILogger _logger;

    private static readonly Argument<string> ProjectNameArg =
        new("project", "The name of the project to generate icons for");

    public IconCommand(NeonConfigManager config, ILogger logger)
    {
        _config = config;
        _logger = logger;
    }


    private void GenerateSvgIcon(ProjectConfig project)
    {
        string destination = "icons_";
        SvgDocument doc = SvgDocument.Open("Templates/icon.svg");
        doc.GetElementById<SvgText>("Key").Text = FormatKey(project.Key);
        SvgGradientServer svgGradientServer = doc.GetElementById<SvgGradientServer>("_Linear1");
        string background = project.IconBackground;
        string color1 = background.Split('|')[0];
        string color2 = background.Split('|')[1];
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
            string savePath = Path.Join(destination, $"{project.Key}_{iconSize}.png");
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
            GenerateSvgIcon(project);
        }
    }

    protected override Task<int> ExecuteCommand(CliCommandContext context)
    {
        string projectArg = context.Argument<string>(ProjectNameArg).ToLower();
        if (projectArg == "all" || projectArg == "*")
            GenerateAllIcons();
        else
            GenerateSvgIcon(_config.GetProjectByKey(projectArg));

        return Task.FromResult(0);
    }
}