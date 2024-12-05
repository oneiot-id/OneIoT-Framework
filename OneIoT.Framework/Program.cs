// using System.Diagnostics;

using System.Diagnostics;
using OneIoT.Framework.Configuration;
using OneIoT.Framework.Graphics;
using OneIoT.Framework.Graphics.Renderer;
using OneIoT.Framework.Graphics.Shapes;
using OneIoT.Framework.Graphics.VisualElements;
using OneIoT.Framework.Graphics.Windowing;

namespace OneIoT.Framework
{
    public class Program
    {
        public static void Main()
        {
            // Console.WriteLine(ScreenInfo.ScreenHeight);
            
            // using (Game game = new Game(1000, 1000, "Window"))
            // {
            //     game.Run();
            // }
            
            
            


            using (Window window = new Window(1000, 1000, "Window"))
            {
                GLRenderer glRenderer = new GLRenderer(window);
                Triangle triangle = new Triangle(Anchors.MiddleCenter, new Size(){Width = 100, Height = 100});
                
                glRenderer.Render(triangle);
                window.Run();
            }
            
            // glRenderer.Render(triangle);
            
        }
    }
}