namespace OneIoT.Framework.Utils;

public class MathHelper
{
    public static float Map(float value, float fromMin, float fromMax, float toMin, float toMax) => toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
}