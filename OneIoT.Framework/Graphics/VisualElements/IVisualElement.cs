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
    
    /// <summary>
    /// Anchors is the center point of the object
    /// </summary>
    public Anchors Anchor { get; set; }
    
    /// <summary>
    /// Origins is the center point of the parent object, only can used when on relative <see cref="Transform.Positioning"/>
    /// </summary>
    public Anchors Origin { get; set; }

    public Vector2 CenterPoint { get; set; }

    public bool Visible { get; set; }

    public void AddChild(IVisualElement element);
    
    public delegate void ClickHandler(Func<object> e);
}