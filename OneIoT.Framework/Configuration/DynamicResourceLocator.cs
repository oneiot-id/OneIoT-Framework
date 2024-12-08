using System.Diagnostics;

namespace OneIoT.Framework.Configuration;

public class DynamicResourceLocator
{
    public static string ApplicationPath = Directory.GetCurrentDirectory();
    public static string GetApplicationPath = BackInDirectory(3);

    public static string BackInDirectory(int iterator, string? path = null)
    {
        string currentDir = (path) ?? ApplicationPath;
        
        for (int i = 0; i < iterator; i++)
        {
            //back a parent
            string? info = Directory.GetParent(currentDir)?.ToString();
            currentDir = info;
        }
        
        return currentDir ?? ApplicationPath;
    }

    public static string ShaderPath = $@"{GetApplicationPath}\Graphics\Renderer\shader.vert";
    public static string FragmentPath = $@"{GetApplicationPath}\Graphics\Renderer\shader.frag";
}