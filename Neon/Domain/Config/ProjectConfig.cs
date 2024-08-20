namespace Neon.Domain.Config;

public class ProjectConfig
{
    public string Key { get; set; }
    public string Name { get; set; }
    public string GitHubRepo { get; set; }
    public string IconGradientColor1 { get; set; }
    public string IconGradientColor2 { get; set; }
    
    public ProjectConfig()
    {
    }
    public ProjectConfig(string key, string name,string gitHubRepo = "", string iconGradientColor1 = "", string iconGradientColor2 = "")
    {
        Key = key;
        Name = name;
        GitHubRepo = gitHubRepo;
        IconGradientColor1 = iconGradientColor1;
        IconGradientColor2 = iconGradientColor2;
    }
}