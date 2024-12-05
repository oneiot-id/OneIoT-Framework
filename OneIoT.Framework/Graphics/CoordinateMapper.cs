using OpenTK.Mathematics;

namespace OneIoT.Framework.Graphics;

public class CoordinateMapper
{
    public static float NormalizeX(float value, float maxValue) => MathHelper.MapRange(value, 0f, maxValue, -1f, 1f);
    
    // public static float NormalizeX(float value, float maxValue) => MathHelper.MapRange(value, 0f, maxValue, -1f, 1f);
    //
    // public static float NormalizeX(float value, float maxValue) => MathHelper.MapRange(value, 0f, maxValue, -1f, 1f);
    //
    public static float NormalizeY(float value, float maxValue) => MathHelper.MapRange(value, 0f, maxValue, 1f, -1f);
                        
}
