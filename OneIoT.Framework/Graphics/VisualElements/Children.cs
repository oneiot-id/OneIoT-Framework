namespace OneIoT.Framework.Graphics.VisualElements;

public class Children
{
    // public IVisualElement Parent { get; set; }
    public List<IVisualElement> Child = new List<IVisualElement>();

    public Children()
    {
        
    }

    public void AddChildren(IVisualElement element)
    {
        Child.Add(element);
    }
}