using OpenTK.Mathematics;

namespace OneIoT.Framework.Graphics;

public class Point
{
    public Vector2 Position { get; set; } = new Vector2();
    
    public Point(Vector2 point)
    {
        Position = point;
    }

    public Point()
    {
        
    }

}