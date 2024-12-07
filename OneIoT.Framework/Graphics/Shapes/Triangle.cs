using OneIoT.Framework.Graphics.Drawable;
using OneIoT.Framework.Graphics.VisualElements;
using OpenTK.Mathematics;

namespace OneIoT.Framework.Graphics.Shapes;


public class Triangle : VisualElement
{
    public sealed override Vector2[] Points { get; set; } = new Vector2[3];

    public Triangle()
    {
        base.Anchor = Anchors.MiddleCenter;
        base.Size = new Size() { Width = 100, Height = 100 };
    }

    public Triangle(IVisualElement parent) : base(parent)
    {
        
    }
    
    public Triangle(Anchors createAnchor, Size size)
    {
        base.Anchor = createAnchor;
        base.Size = size;
    }
}