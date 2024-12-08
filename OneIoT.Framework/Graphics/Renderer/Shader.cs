using System.Globalization;
using OneIoT.Framework.Configuration;
using OneIoT.Framework.Graphics.Color;
using OneIoT.Framework.Graphics.VisualElements;
using OpenTK.Graphics.OpenGL4;

namespace OneIoT.Framework.Graphics.Renderer;

public class Shader : IDisposable
{
    public int Handle;
    private int _vertexShader;
    private int _fragmentShader;

    public Shader()
    {
        var shaderVert = File.ReadAllText(DynamicResourceLocator.ShaderPath);
        var shaderFrag = File.ReadAllText(DynamicResourceLocator.FragmentPath);

        _vertexShader = GL.CreateShader(ShaderType.VertexShader);
        _fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        
        GL.ShaderSource(_vertexShader, shaderVert);
        GL.ShaderSource(_fragmentShader, shaderFrag);
        
        GL.CompileShader(_vertexShader);
        GL.CompileShader(_fragmentShader);
        
        GL.GetShader(_vertexShader, ShaderParameter.CompileStatus, out var s);
        GL.GetShader(_fragmentShader, ShaderParameter.CompileStatus, out var s2);

        if (s == 0 || s2 == 0)
        {
            string infoLog = GL.GetShaderInfoLog(_vertexShader);
            Console.WriteLine(infoLog);
        }

        Handle = GL.CreateProgram();
        GL.AttachShader(Handle, _vertexShader);
        GL.AttachShader(Handle, _fragmentShader);
        
        GL.LinkProgram(Handle);
        
        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success3);

        if (success3 == 0)
        {
            string infoLog = GL.GetProgramInfoLog(Handle);
            Console.WriteLine(infoLog);
        }

        GL.DetachShader(Handle, _vertexShader);
        GL.DetachShader(Handle, _fragmentShader);
        GL.DeleteShader(_fragmentShader);
        GL.DeleteShader(_vertexShader);
    }
    
    public Shader(IVisualElement? element = null)
    {
        var bgColor = element?.Color.BackgroundColor ?? new Colour();

        var red = bgColor.Red.ToString("0.0", CultureInfo.InvariantCulture);
        var green =bgColor.Green.ToString("0.0", CultureInfo.InvariantCulture);
        var blue = bgColor.Blue.ToString("0.0", CultureInfo.InvariantCulture);
        var alpha = bgColor.Alpha.ToString("0.0", CultureInfo.InvariantCulture);

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
                                FragColor =" + $" vec4({red}f, {green}f, {blue}f, {alpha}f);" + @"
                             }";

        _vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(_vertexShader, shaderVert);

        _fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(_fragmentShader, shaderFrag);

        GL.CompileShader(_vertexShader);

        GL.GetShader(_vertexShader, ShaderParameter.CompileStatus, out var success);
        // GL.GetShader();

        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(_vertexShader);
            Console.WriteLine(infoLog);
        }

        GL.CompileShader(_fragmentShader);

        GL.GetShader(_fragmentShader, ShaderParameter.CompileStatus, out var success2);

        if (success2 == 0)
        {
            string infoLog = GL.GetShaderInfoLog(_fragmentShader);
            Console.WriteLine(infoLog);
        }

        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, _vertexShader);
        GL.AttachShader(Handle, _fragmentShader);

        GL.LinkProgram(Handle);

        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success3);

        if (success3 == 0)
        {
            string infoLog = GL.GetProgramInfoLog(Handle);
            Console.WriteLine(infoLog);
        }

        GL.DetachShader(Handle, _vertexShader);
        GL.DetachShader(Handle, _fragmentShader);
        GL.DeleteShader(_fragmentShader);
        GL.DeleteShader(_vertexShader);
    }


    public Shader(string vertexCode, string fragmentCode)
    {
        _vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(_vertexShader, vertexCode);

        _fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(_fragmentShader, fragmentCode);

        GL.CompileShader(_vertexShader);

        GL.GetShader(_vertexShader, ShaderParameter.CompileStatus, out var success);
        // GL.GetShader();

        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(_vertexShader);
            Console.WriteLine(infoLog);
        }

        GL.CompileShader(_fragmentShader);

        GL.GetShader(_fragmentShader, ShaderParameter.CompileStatus, out var success2);

        if (success2 == 0)
        {
            string infoLog = GL.GetShaderInfoLog(_fragmentShader);
            Console.WriteLine(infoLog);
        }

        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, _vertexShader);
        GL.AttachShader(Handle, _fragmentShader);

        GL.LinkProgram(Handle);

        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success3);

        if (success3 == 0)
        {
            string infoLog = GL.GetProgramInfoLog(Handle);
            Console.WriteLine(infoLog);
        }

        GL.DetachShader(Handle, _vertexShader);
        GL.DetachShader(Handle, _fragmentShader);
        GL.DeleteShader(_fragmentShader);
        GL.DeleteShader(_vertexShader);
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