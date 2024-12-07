using OneIoT.Framework.Graphics.Drawable;
using OpenTK.Mathematics;

namespace OneIoT.Framework.Graphics.VisualElements;

public interface IVisualElement : IDrawable
{   
    public string Name { get; set; }
    
    public IVisualElement Parent { get; set; }
    
    public Children Children { get; set; }
    
    public Transform Transform { get; set; }
    
    public Size Size { get; set; }
    
    public VisualElementColor Color { get; set; }
    
    public Anchors Anchor { get; set; }
    
    public Anchors Origin { get; set; }

    public Vector2 CenterPoint { get; set; }

    public bool Visible { get; set; }

    public void AddChild(IVisualElement element);

    // public List<Point> Points { get; set; }
    
    public delegate void ClickHandler(Func<object> e);
    
}