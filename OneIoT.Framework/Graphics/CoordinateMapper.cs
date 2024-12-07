using OneIoT.Framework.Graphics.Windowing;
using OpenTK.Mathematics;
using OneIoT.Framework.Graphics.Windowing;
using OpenTK.Mathematics;

namespace OneIoT.Framework.Graphics;

public static class CoordinateMapper
{
    public static float NormalizeX(float value, float maxValue) => MathHelper.MapRange(value, 0f, maxValue, -1f, 1f);
    
    public static float NormalizeY(float value, float maxValue) => MathHelper.MapRange(value, 0f, maxValue, 1f, -1f);

    public static Vector2 Normalize(Vector2 position, Vector2 maxValue) => new(NormalizeX(position.X, maxValue.X), NormalizeY(position.Y, maxValue.Y));

    public static Vector2 ActualPosition(Window window, Vector2 position )
    {
        var xPos = MathHelper.MapRange(position.X, -1f, 1f, 0, window.Size.Width);
        var yPos = MathHelper.MapRange(position.Y, 1f, -1f, 0, window.Size.Height);
        
        return new Vector2(xPos, yPos);
    }
}

