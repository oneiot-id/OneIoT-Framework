using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OneIoT.Framework.Graphics;

public class Game : GameWindow
{
    private Shader _shader;
    
    float[] vertices = {
        -0.5f, -0.5f, 0.0f, //Bottom-left vertex
        0.5f, -0.5f, 0.0f, //Bottom-right vertex
        0.0f,  0.5f, 0.0f  //Top vertex
    };
    
    float[] vertices2 = {
        -0.5f, -0.5f, 0.0f, //Bottom-left vertex
        0.5f, -0.5f, 0.0f, //Bottom-right vertex
        0.0f,  0.5f, 0.0f  //Top vertex
    };
    
    int VertexBufferObject;
    private int VertexBufferObject2;
    
    public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
            
    }

    public Game(int width, int height, string title) : base(GameWindowSettings.Default,
        new NativeWindowSettings() { Size = (width, height), Title = title })
    {
        vertices2[0] = CoordinateMapper.NormalizeX(348.45f, width);
        vertices2[1] = CoordinateMapper.NormalizeY(237f, width);
        
        vertices2[3] = CoordinateMapper.NormalizeX(651.55f, width);
        vertices2[4] = CoordinateMapper.NormalizeY(237.5f, width);
        
        vertices2[6] = CoordinateMapper.NormalizeX(500f, width);
        vertices2[7] = CoordinateMapper.NormalizeY(500f, width);
        
    }
    
    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);
    
        GL.Viewport(0, 0, e.Width, e.Height);
    }

    private void MakeTriangle(float[] coordinates)
    {
        int vbo = GL.GenBuffer();
        int vao = GL.GenVertexArray();
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, coordinates.Length * sizeof(float), coordinates, BufferUsageHint.StaticDraw);
        
        GL.BindVertexArray(vao);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        
        GL.EnableVertexAttribArray(0);
        
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
    }

    private void RenderTriangle()
    {
        _shader.Use();
    }
    
    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit);


        //Code goes here.
        
        MakeTriangle(vertices);
        MakeTriangle(vertices2);
        RenderTriangle();
        
        // GL.BindVertexArray(VertexBufferObject);

        SwapBuffers();
        
        


        
        // GL.UseProgram();
    }
    
    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        //Code goes here
        
        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        
        string shaderVert = @"#version 330 core
                            layout (location = 0) in vec3 aPosition;

                            void main()
                            {
                                gl_Position = vec4(aPosition, 1.0);
                            }";

        string shaderFrag = @"#version 330 core
                            out vec4 FragColor;

                            void main()
                            {
                                FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
                            }";

        _shader = new Shader(shaderVert, shaderFrag);

        int VertexArrayObject;
        
        VertexArrayObject = GL.GenVertexArray();
        
        GL.BindVertexArray(VertexArrayObject);
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

    }
    
    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        
        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();        
        }
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        _shader.Dispose();
        
    }

    public class Shader : IDisposable
    {
        int Handle;
        private int VertexShader;
        private int FragmentShader ;

        
        public Shader(string vertexCode, string fragmentCode)
        {
            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, vertexCode);

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, fragmentCode);
            
            GL.CompileShader(VertexShader);

            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out var success);
            // GL.GetShader();

            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(VertexShader);
                Console.WriteLine(infoLog);
            }
            
            GL.CompileShader(FragmentShader);
            
            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out var success2);

            if (success2 == 0)
            {
                string infoLog = GL.GetShaderInfoLog(FragmentShader);
                Console.WriteLine(infoLog);
            }

            Handle = GL.CreateProgram();
            
            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);
            
            GL.LinkProgram(Handle);
            
            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success3);

            if (success3 == 0)
            {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Console.WriteLine(infoLog);
            }
            
            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);
            

        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        private bool disposedValue = false;
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            if (disposedValue == false)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}