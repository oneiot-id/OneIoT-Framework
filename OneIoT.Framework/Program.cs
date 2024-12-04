// using System.Diagnostics;

using System.Diagnostics;
using OneIoT.Framework.Graphics;

namespace OneIoT.Framework
{
    public class Program
    {
        public static void Main()
        {
            using (Game game = new Game(1000, 1000, "Window"))
            {
                game.Run();
            }
        
        
        }

        public static void Run()
        {
            // Console.WriteLine(Transform.ToNormalX(500, 1000));
            
        }

        public static void Debug()
        {
            
        }
    }
}