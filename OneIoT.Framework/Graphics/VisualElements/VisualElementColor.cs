using System.Numerics;
using OneIoT.Framework.Graphics.Color;

namespace OneIoT.Framework.Graphics.VisualElements;

public class VisualElementColor
{
    public  Colour BackgroundColor  = new Colour();
    public Colour TextColor = new Colour();

    public VisualElementColor(Colour backgroundColor, Colour textColor)
    {
        BackgroundColor = backgroundColor;
        TextColor = textColor;
    }

    public VisualElementColor()
    {
        
    }
}