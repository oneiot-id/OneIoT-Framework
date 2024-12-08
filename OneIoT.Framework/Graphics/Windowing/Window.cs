using System.Diagnostics;
using System.Runtime.CompilerServices;
using OneIoT.Framework.Events;
using OneIoT.Framework.Graphics.Color;
using OneIoT.Framework.Graphics.Renderer;
using OneIoT.Framework.Graphics.Shapes;
using OneIoT.Framework.Graphics.VisualElements;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;


// #pragma warning disable CS0618 // Type or member is obsolete
namespace OneIoT.Framework.Graphics.Windowing;

public class Window : GameWindow, IVisualElement
{
    public GLRenderer GlRenderer;
    public Event Event;
    
    public event Action UpdateFrameEvent;
    public event Action MouseClickedEvent;
    
    public Window(int width, int height, string windowTitle)
        : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = windowTitle })
    {
        GlRenderer = new GLRenderer(this);
        Event = new Event(this);
        
        Size = new Size(){Width = height, Height = height};
        CenterPoint = new Vector2(Size.Width / 2, Size.Height / 2);
        
        UpdateFrameEvent += GlRenderer.Render;
        MouseClickedEvent += Event.HandleMouseDown;
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
    }

    private void UpdateChildren(IVisualElement visualElement)
    {
        visualElement.CenterPoint = CenterPoint;

        foreach (var child in visualElement.Children.Child)
        {
            UpdateChildren(child);
        }
    }
    
    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        
        Size = new Size(){Width = e.Width, Height = e.Height};
        
        CenterPoint = new Vector2(Size.Width / 2, Size.Height / 2);

        GL.Viewport(0, 0, e.Width, e.Height);

        foreach (var child in Children.Child)
        {
            UpdateChildren(child);
        }
    }

    // private Box box;

    Triangle t1 = new Triangle()
    {
        Name = "Triangle 1",
        CenterPoint = new Vector2(500, 500),
        Color = new VisualElementColor()
        {
            BackgroundColor = new Colour(0x7F20BA)
        },
        Size = new Size()
        {
            Width = 200,
            Height = 200
        }
    };
        
    Triangle t2 = new Triangle()
    {
        CenterPoint = new Vector2(100, 100),
        Size = new Size()
        {
            Width = 50,
            Height = 50
        },
        Color = new VisualElementColor()
        {
            BackgroundColor = new Colour(255f, 255f, 255f, 100f)
        },
        // Visible = false
    };

    private bool visible = false;
    
    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        foreach (var child in Children.Child)
        {
            GlRenderer.AddToRenderQueue(child);
        }
        
        OnUpdateFrameEvent();
        OnMouseEvent();
        
        //We need to adjust this based on the display rate (hz)
        //We also need to do another method, with threading 
        Thread.Sleep(1);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);


    }
    
    protected virtual void OnUpdateFrameEvent()
    {
        UpdateFrameEvent?.Invoke();
    }

    public string Name { get; set; }
    
    public IVisualElement Parent { get; set; }

    public Children Children { get; set; } = new Children();
    
    public Transform Transform { get; set; }
    
    public Size Size { get; set; }
    
    public VisualElementColor Color { get; set; }
    
    public Anchors Anchor { get; set; }
    public Anchors Origin { get; set; }

    public Vector2 CenterPoint { get; set; }
    
    public bool Visible { get; set; }

    public void AddChild(IVisualElement element)
    {
        Children.AddChildren(element);
    }

    public List<Point> Points { get; set; }

    public void OnClick(Func<object> e)
    {
        
    }

    protected virtual void OnMouseEvent()
    {
        MouseClickedEvent?.Invoke();
    }
}
