namespace Neon.Domain.Config;

public class IconConfig
{
    public int[] Sizes { get; set; } = new[] { 16, 32, 64, 128, 256, 512 };
    public string[] Formats { get; set; } = new[] { "png", "ico", "svg" };
}