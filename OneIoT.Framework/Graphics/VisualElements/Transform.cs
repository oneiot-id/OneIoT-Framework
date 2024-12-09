using OneIoT.Framework.Graphics.Transformation;
using OpenTK.Mathematics;

namespace OneIoT.Framework.Graphics.VisualElements;

public class Transform
{
    public Positioning Positioning { get; set; }
    
    public Vector2 Position { get; set; }

    public float Scale { get; set; }

    public float Rotation { get; set; }
}