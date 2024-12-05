using OneIoT.Framework.Graphics.Renderer;
using OneIoT.Framework.Graphics.VisualElements;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

#pragma warning disable CS0618 // Type or member is obsolete
namespace OneIoT.Framework.Graphics.Windowing;

public class Window : GameWindow
{
    public bool IsLoaded { get; private set; }
    public float ScreenWidth { get; private set; }
    public float ScreenHeight { get; private set; }
    public Queue<Action> RendererQueue { get; } = new Queue<Action>();

    public Window(int width, int height, string windowTitle)
        : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = windowTitle })
    {
        ScreenWidth = width;
        ScreenHeight = height;
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        IsLoaded = true;
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Process all rendering tasks in the queue
        while (RendererQueue.Count > 0)
        {
            var renderTask = RendererQueue.Dequeue();
            renderTask?.Invoke();
            
            GLRenderer.UseShader(); // Re-bind shader
        }

        GLRenderer.RenderTri();
        
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
    }
}
