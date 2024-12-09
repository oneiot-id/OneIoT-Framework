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
using OpenTK.Windowing.Common.Input;
using StbImageSharp;
using Vector2 = OpenTK.Mathematics.Vector2;

namespace OneIoT.Framework
{
    public class Program
    {
        private static readonly Lock _balanceLock = new Lock();
        
        /// <summary>
        /// This is the starting point of the program, you can follow the process of making an UI with this framework
        /// </summary>
        public static void Main()
        {
            Window window = new Window(1000, 1000, "One.IoT Framework [Development]");

            Box box = new Box(window)
            {
                Size = new Size()
                {
                    Width = 100,
                    Height = 100
                },
                Color = new VisualElementColor()
                {
                    BackgroundColor = new Colour(0xFFFFFF)
                },
                Transform = new Transform()
                {
                    Position = new Vector2(-100, 0)
                }
            };
            
            
            window.AddChild(box);
            window.Run();
            window.Dispose();
            // Thread t1 = new Thread(new ThreadStart(Main2));
            // Thread t2= new Thread(new ThreadStart(Main3));
            //
            // t1.Start();
            // t2.Start();

            
            
            

        }

        public static void Main2()
        {
            while (true)
            {
                Test("from main 2");
                // Console.WriteLine("main 2");
            }
        }
        
        public static void Main3()
        {
            while (true)
            {
                Test("from main 3");
                // Console.WriteLine("main 3");
            }
        }

        public static void Test(string msg)
        {
            // lock (_balanceLock)
            // {
                Console.WriteLine(msg);
                Thread.Sleep(1000);
            // }
        }
    }
    
}