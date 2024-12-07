using System.Runtime.InteropServices;
using System.Security.Principal;
using OpenTK.Windowing.Desktop;
using SharpDX;

namespace OneIoT.Framework.Configuration;

public class ScreenInfo
{
    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);
    
    public static float ScreenWidth { get; set; }
    
    public static float ScreenHeight { get; set; }
    
    public static float DisplayRate { get; set; }

    static ScreenInfo()
    {
        ScreenWidth = GetSystemMetrics(0);
        ScreenHeight = GetSystemMetrics(1);
        
        
    }
}