namespace Neon.Domain.Config;

public class GenesisConfig
{
    public List<ProjectConfig> Projects { get; set; } = new()

    {
        new("ne", "Gensys", "armunro/gensys", "#EFEFEF", "#EFEFEF"),
        new("co", "Cosmic"),
        new("ti", "Titanium", "armunro/titanium"),
        new("at", "Atom", "armunro/atom-core"),
        new("am", "AMME", "armunro/atom-core"),
        new("wd", "WarpDeck", "armunro/warpdeck"),
        new("yt", "Yttrium"),
    };
    public IconConfig Icon { get; set; } = new();
}