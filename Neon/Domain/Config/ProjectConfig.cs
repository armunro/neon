namespace Neon.Domain.Config;

public class ProjectConfig
{
    public string Key { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Repo { get; set; }
    public string WorkingCopy { get; set; }
    public string IconBackground { get; set; }
    public string IconGradientColor2 { get; set; }
    
    public ProjectConfig()
    {
    }
    public ProjectConfig(string key, string name,string repo = "", string iconBackground = "", string iconGradientColor2 = "")
    {
        Key = key;
        Name = name;
        Repo = repo;
        IconBackground = iconBackground;
        IconGradientColor2 = iconGradientColor2;
    }
}   