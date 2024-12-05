namespace OneIoT.Framework.Graphics.VisualElements;

public abstract class VisualElement : IVisualElement
{
    public Transform Transform { get; set; } = new Transform();

    public Size Size { get; set; } = new Size();

    public VisualElementColor Color { get; set; } = new VisualElementColor();
    
    public Anchors Anchors { get; set; }

    public virtual void Load()
    {
        
    }
}