using OpenTK.Mathematics;

namespace OneIoT.Framework.Graphics.VisualElements;

public abstract class VisualElement : IVisualElement
{
    public IVisualElement? Parent { get; set; } = null;

    public Children Children { get; set; } = new Children();

    public Transform Transform { get; set; } = new Transform();

    public Size Size { get; set; } = new Size();

    public VisualElementColor Color { get; set; } = new VisualElementColor();
    
    public Anchors Anchors { get; set; }

    public Vector2? CenterPoint { get; set; } = null;

    public void AddChild(IVisualElement element)
    {
        element.Parent = this;
        Children.AddChildren(element);
    }
}