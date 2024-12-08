namespace OneIoT.Framework.Graphics.Color;

public class Colour
{
    public float Red { get; set; } = 0.0f;
    public float Green { get; set; } = 0.0f;
    public float Blue { get; set; } = 0.0f;
    public float Alpha { get; set; } = 1.0f;

    public Colour()
    {
        
    }

    /// <summary>
    /// Create new colour on (0 - 255) and Alpha (0 - 100)
    /// </summary>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <param name="alpha"></param>
    public Colour(float red = 0f, float green = 0f, float blue = 0f, float alpha = 1f)
    {
        Red = Math.Clamp(red / 255f, 0, 1f);
        Green = Math.Clamp(green / 255f, 0f, 1f);
        Blue = Math.Clamp(blue / 255f, 0f, 1f);
        Alpha = Math.Clamp(alpha / 100f, 0, 1f);
    }

    /// <summary>
    /// Create a colour with hexadecimal value
    /// </summary>
    /// <param name="color"></param>
    public Colour(long color, float alpha)
    {
        Red = (float) (color & 0xFF0000) / 0xFF0000;
        Green =  (float) (color & 0x00FF00) / 0x00FF00;
        Blue= (float) (color & 0x0000FF) / 0x0000FF;
        Alpha = Math.Clamp(alpha / 100f, 0f, 1f);
    }
}