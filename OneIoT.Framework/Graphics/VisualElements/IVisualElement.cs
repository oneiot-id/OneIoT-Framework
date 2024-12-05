using OneIoT.Framework.Graphics.Drawable;

namespace OneIoT.Framework.Graphics.VisualElements;

public interface IVisualElement : IDrawable
{
    public Transform Transform { get; set; }
    
    public Size Size { get; set; }
    
    public VisualElementColor Color { get; set; }
    
    public Anchors Anchors { get; set; }
}