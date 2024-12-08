using OneIoT.Framework.Graphics.VisualElements;
using OpenTK.Mathematics;

namespace OneIoT.Framework.Graphics.Shapes;

public class Box : VisualElement
{
    public override Vector2[] Points { get; set; } = new Vector2[9];

    public sealed override Vector2 CenterPoint
    {
        get => base.CenterPoint;
        set
        {
            base.CenterPoint = value;
            CalculatePoints();
        }
    }

    public sealed override Size Size
    {
        get => base.Size;
        set
        {
            base.Size = value;
            CalculatePoints();
        }
    }

    public sealed override Transform Transform
    { 
        get => base.Transform;
        set
        {
            if (base.Transform.Position != value.Position)
            {
                CalculateTranslation(value);
            }
            if (base.Transform.Positioning != value.Positioning)
            {
                base.Transform.Positioning = value.Positioning;
            }

            if (base.Transform.Scale != value.Scale)
            {
                base.Transform.Scale = value.Scale;
                base.CalculateScaling(value);
                CalculatePoints();
            }
        }
    }

    public override Anchors Anchor
    {
        get => base.Anchor;
        set
        {
            base.Anchor = value;
            CalculateAnchor();
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
        var cX = base.Parent.CenterPoint.X + PositionOffset.X;
        var cY = base.Parent.CenterPoint.Y + PositionOffset.Y;
        
        var hWidth = base.Size.Width / 2f;
        var hHeight = base.Size.Height / 2f;

        //Top
        //Top Left
        Points[(int) Anchors.TopLeft] = new Vector2(cX - hWidth, cY - hHeight);
        //Top Center
        Points[(int) Anchors.TopCenter] = new Vector2(cX, cY - hHeight);
        //Top Right
        Points[(int) Anchors.TopRight] = new Vector2(cX + hWidth, cY - hHeight);
        
        //Middle
        //Left
        Points[(int) Anchors.MiddleLeft] = new Vector2(cX - hWidth,  cY);
        //Center
        Points[(int) Anchors.MiddleCenter] = new Vector2(cX ,  cY);
        //Right
        Points[(int) Anchors.MiddleRight] = new Vector2(cX + hWidth ,  cY);
        
        //Bottom
        //Left
        Points[(int) Anchors.BottomLeft] = new Vector2(cX - hWidth, cY + hHeight);
        //Center
        Points[(int) Anchors.BottomCenter] = new Vector2(cX, cY + hHeight);
        // Right
        Points[(int) Anchors.BottomRight] = new Vector2(cX + hWidth, cY + hHeight);
    }

    protected sealed override void  CalculateAnchor()
    {
        var halfW = base.Size.Width / 2f;
        var halfH = base.Size.Height / 2f;
        
        switch (Anchor)
        {
            case Anchors.TopLeft:
                PositionOffset.X += halfW;
                PositionOffset.Y += halfH;
                break;
            case Anchors.TopCenter:
                PositionOffset.Y += halfH;
                break;
            case Anchors.TopRight:
                PositionOffset.X -= halfW;
                PositionOffset.Y += halfH;
                break;
            case Anchors.MiddleLeft:
                PositionOffset.X += halfW;
                break;
            case Anchors.MiddleCenter:
                break;
            case Anchors.MiddleRight:
                PositionOffset.X -= halfW;
                break;
            case Anchors.BottomLeft:
                PositionOffset.X += halfW;
                PositionOffset.Y -= halfH;
                break;
            case Anchors.BottomCenter:
                PositionOffset.Y -= halfH;
                break;
            case Anchors.BottomRight:
                PositionOffset.X -= halfW;
                PositionOffset.Y -= halfH;
                break;
        }
    }
}