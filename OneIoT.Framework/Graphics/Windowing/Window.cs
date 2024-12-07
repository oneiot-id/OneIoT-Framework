using System.Diagnostics;
using System.Runtime.CompilerServices;
using OneIoT.Framework.Graphics.Color;
using OneIoT.Framework.Graphics.Renderer;
using OneIoT.Framework.Graphics.Shapes;
using OneIoT.Framework.Graphics.VisualElements;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

#pragma warning disable CS0618 // Type or member is obsolete
namespace OneIoT.Framework.Graphics.Windowing;

public class Window : GameWindow, IVisualElement
{
    public bool IsLoaded { get; private set; }
    public float ScreenWidth { get; private set; }
    public float ScreenHeight { get; private set; }

    public GLRenderer GlRenderer;
    
    public event Action UpdateFrameEvent;
    
    public Window(int width, int height, string windowTitle)
        : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = windowTitle })
    {
        
        ScreenWidth = width;
        ScreenHeight = height;
        
        GlRenderer = new GLRenderer(this);
        
        Size = new Size(){Width = width, Height = height};
        CenterPoint = new Vector2(Size.Width / 2, Size.Height / 2);
        
        UpdateFrameEvent += GlRenderer.Render;
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        
        IsLoaded = true;
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        
        Size = new Size(){Width = e.Width, Height = e.Height};
        
        CenterPoint = new Vector2(Size.Width / 2, Size.Height / 2);

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        Triangle t1 = new Triangle()
        {
            CenterPoint = this.CenterPoint,
            Color = new VisualElementColor()
            {
                BackgroundColor = new Colour(0x7F20BA)
            },
            Size = new Size()
            {
                Width = 200f,
                Height = 200f
            }
        };
        
        Triangle t2 = new Triangle()
        {
            CenterPoint = new Vector2(100, 100),
            Size = new Size()
            {
                Width = 50f,
                Height = 50f
            },
            Color = new VisualElementColor()
            {
                BackgroundColor = new Colour(255f, 255f, 255f, 100f)
            }
        };
        
        // t1.AddChild(t2);
        
        GlRenderer.AddToRenderQueue(t1);
        GlRenderer.AddToRenderQueue(t2);
        OnUpdateFrameEvent();
        
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

    }
    
    protected virtual void OnUpdateFrameEvent()
    {
        UpdateFrameEvent?.Invoke();
    }

    public IVisualElement Parent { get; set; }
    
    public Children Children { get; set; }

    // public List<VisualElement> Children { get; set; }
    
    public Transform Transform { get; set; }
    
    public Size Size { get; set; }
    
    public VisualElementColor Color { get; set; }
    
    public Anchors Anchors { get; set; }
    
    public Vector2? CenterPoint { get; set; }

    public void AddChild(IVisualElement element)
    {
        
    }
}
