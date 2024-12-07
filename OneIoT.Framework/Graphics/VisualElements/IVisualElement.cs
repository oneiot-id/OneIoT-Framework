using OneIoT.Framework.Graphics.Drawable;
using OpenTK.Mathematics;

namespace OneIoT.Framework.Graphics.VisualElements;

public interface IVisualElement : IDrawable
{
    public IVisualElement Parent { get; set; }
    
    public Children Children { get; set; }
    
    public Transform Transform { get; set; }
    
    public Size Size { get; set; }
    
    public VisualElementColor Color { get; set; }
    
    public Anchors Anchors { get; set; }

    public Vector2? CenterPoint { get; set; }

    public void AddChild(IVisualElement element);
}