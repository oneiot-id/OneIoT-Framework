// using System.Diagnostics;

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using OneIoT.Framework.Configuration;
using OneIoT.Framework.Graphics;
using OneIoT.Framework.Graphics.Color;
using OneIoT.Framework.Graphics.Renderer;
using OneIoT.Framework.Graphics.Shapes;
using OneIoT.Framework.Graphics.VisualElements;
using OneIoT.Framework.Graphics.Windowing;
using OneIoT.Framework.IO.Images;

namespace OneIoT.Framework
{
    public class Program
    {
        
        /// <summary>
        /// This is the starting point of the program, you can follow the process of making an UI with this framework
        /// </summary>
        public static void Main()
        {
            // Window window = new Window(1000, 1000, "One.IoT Framework [Development]");
            //
            // Box box = new Box(window)
            // {
            //     Name = "Clickable",
            //     Anchor = Anchors.MiddleCenter,
            //     Size = new Size()
            //     {
            //         Width = 100f,
            //         Height = 100f
            //     },
            //     Transform = new Transform()
            //     {
            //         Scale = 1.5f,
            //         // Position = new Vector2(50, 50)
            //     }
            // };
            //
            // Box box1 = new Box(window)
            // {
            //     Name = "Clickable2",
            //     Color = new VisualElementColor()
            //     {
            //         BackgroundColor = new Colour(0xFFFFFF, 100)
            //     }
            // };
            //     
            //
            // box.AddChild(box1);
            // window.AddChild(box);

            string path = @"D:\OneIoT Framework\Assets\aaa.png";
            PngReader png = new PngReader(path);

            // window.Run();
            // window.Dispose();
        }
    }
    
}