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
        
        /// <summary>
        /// This is the starting point of the program, you can follow the process of making an UI with this framework
        /// </summary>
        public static void Main()
        {
            Window window = new Window(1000, 1000, "One.IoT Framework [Development]");
          
            Box box = new Box(window)
            {
                Name = "Clickable",
                Size = new Size()
                {
                    Width = 100f,
                    Height = 100f
                }
            };
            
            
            window.AddChild(box);
            window.Run();
            window.Dispose();
        }
    }
    
}