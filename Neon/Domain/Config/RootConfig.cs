namespace Neon.Domain.Config;

public class RootConfig
{
    public List<ProjectConfig> Projects { get; set; } = new() { };
    public string IdePath { get; set; } = "";

    public IconConfig Icon { get; set; } = new();
}