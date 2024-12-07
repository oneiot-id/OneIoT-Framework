using OneIoT.Framework.Graphics.VisualElements;
using OpenTK.Mathematics;

namespace OneIoT.Framework.Graphics.Shapes;

public class Box : VisualElement
{
    public override Vector2[] Points { get; set; } = new Vector2[9];

    public override Vector2 CenterPoint
    {
        get => base.CenterPoint;
        set
        {
            base.CenterPoint = value;
            CalculatePoints();
        }
    }

    public override Size Size
    {
        get => base.Size;
        set
        {
            base.Size = value;
            CalculatePoints();
        }
    } 

    public Box()
    {
        
    }
    
    public Box(IVisualElement parent) : base(parent)
    {   
      // CalculatePoints();  
    }
    
    public Box(IVisualElement parent, Anchors createAnchor, Size size) : base(parent)
    {
        base.Anchor = createAnchor;
        base.Size = size;
    }

    private void CalculatePoints()
    {
        var pX = base.Parent.CenterPoint.X;
        var pY = base.Parent.CenterPoint.Y;
        
        var hWidth = base.Size.Width / 2f;
        var hHeight = base.Size.Height / 2f;

        //Top
        //Top Left
        Points[(int) Anchors.TopLeft] = new Vector2(pX - hWidth, pY - hHeight);
        //Top Center
        Points[(int) Anchors.TopCenter] = new Vector2(pX, pY - hHeight);
        //Top Right
        Points[(int) Anchors.TopRight] = new Vector2(pX + hWidth, pY - hHeight);
        
        //Middle
        //Left
        Points[(int) Anchors.MiddleLeft] = new Vector2(pX - hWidth,  pY);
        //Center
        Points[(int) Anchors.MiddleCenter] = new Vector2(pX ,  pY);
        //Right
        Points[(int) Anchors.MiddleRight] = new Vector2(pX + hWidth ,  pY);
        
        //Bottom
        //Left
        Points[(int) Anchors.BottomLeft] = new Vector2(pX - hWidth, pY + hHeight);
        //Center
        Points[(int) Anchors.BottomCenter] = new Vector2(pX, pY + hHeight);
        // Right
        Points[(int) Anchors.BottomRight] = new Vector2(pX + hWidth, pY + hHeight);
    }
}