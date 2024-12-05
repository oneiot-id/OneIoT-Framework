using System.Numerics;
using OneIoT.Framework.Graphics.Shapes;
using OneIoT.Framework.Graphics.VisualElements;
using OneIoT.Framework.Graphics.Windowing;
using OpenTK.Graphics.OpenGL4;

namespace OneIoT.Framework.Graphics.Renderer;

// ReSharper disable once InconsistentNaming
public class GLRenderer
{
    private readonly Window _window;
    private readonly Game.Shader _basicShader = new Game.Shader();

    private Queue<IVisualElement> _renderQueue = new Queue<IVisualElement>();
    
    public GLRenderer(Window window)
    {
        _window = window;
    }

    public GLRenderer()
    {
        
    }

    private float[] CreateVerticesFromVector(Vector2 position) => new float[] { position.X, position.Y, 0.0f };

    private float[] CreateVerticesFromVectors(List<Vector2> positions)
    {
        float[] vertices = new float[positions.Count];

        for (int i = 0; i < positions.Count; i++)
        {
            float[] vertex = CreateVerticesFromVector(positions[i]);

            vertices[i * 3] = vertex[0]; // X
            vertices[i * 3 + 1] = vertex[1]; // Y
            vertices[i * 3 + 2] = vertex[2]; // Z
        }

        return vertices;
    }

    // private float[] CreateShape(int sides)
    // {
    //         
    // }


    private void RenderTriangle(Triangle triangle)
    {
        float[] vertices = new float[9];

        // Calculate the screen center in normalized coordinates
        float parentCenterWidth = CoordinateMapper.NormalizeX(_window.ScreenWidth / 2f, _window.ScreenWidth);
        float parentCenterHeight =
            CoordinateMapper.NormalizeY(_window.ScreenHeight / 2f,
                _window.ScreenHeight); // Note: Use screen height for Y normalization

        // Calculate the triangle's centroid offsets
        float centroidX = CoordinateMapper.NormalizeX(triangle.Size.Width / 2f, _window.ScreenWidth);
        float centroidY = CoordinateMapper.NormalizeY(triangle.Size.Height / 3f, _window.ScreenHeight);

        // Calculate the three vertices of the triangle
        Vector2 p1 = new Vector2(parentCenterWidth - centroidX, parentCenterHeight - centroidY); // Bottom-left
        Vector2 p2 = new Vector2(parentCenterWidth + centroidX, parentCenterHeight - centroidY); // Bottom-right
        Vector2 p3 = new Vector2(parentCenterWidth, parentCenterHeight + centroidY); // Top

        // Populate the vertices array
        vertices[0] = p1.X;
        vertices[1] = p1.Y;
        vertices[2] = 0.0f; // Vertex 1 (Bottom-left)
        vertices[3] = p2.X;
        vertices[4] = p2.Y;
        vertices[5] = 0.0f; // Vertex 2 (Bottom-right)
        vertices[6] = p3.X;
        vertices[7] = p3.Y;
        vertices[8] = 0.0f; // Vertex 3 (Top)
        
        float[] vertices2 = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f  //Top vertex
        };

        // vertices = vertices2;
        //
        // // Generate and upload vertex data to the GPU
        // int vbo = GL.GenBuffer();
        // int vao = GL.GenVertexArray();
        //
        // GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        // GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        //
        // // Generate and set up the Vertex Array Object (VAO)
        // GL.BindVertexArray(vao);
        // GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        //
        // // Render the triangle
        //
        // // GL.UseProgram(_basicShader.Handle); // Use the shader program
        // GL.EnableVertexAttribArray(0);
        //
        // GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        //
        // Game.Shader shader = new Game.Shader();
        //
        // shader.Use();
        
        int vbo = GL.GenBuffer();
        int vao = GL.GenVertexArray();
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices2.Length * sizeof(float), vertices2, BufferUsageHint.StaticDraw);
        
        GL.BindVertexArray(vao);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        
        GL.EnableVertexAttribArray(0);
        
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

        Game.Shader shader = new Game.Shader();
        
        shader.Use();
    }

    public static void RenderTri()
    {
        float[] vertices2 = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f  //Top vertex
        };
        
        int vbo = GL.GenBuffer();
        int vao = GL.GenVertexArray();
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices2.Length * sizeof(float), vertices2, BufferUsageHint.StaticDraw);
        
        GL.BindVertexArray(vao);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        
        GL.EnableVertexAttribArray(0);
        
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

        Game.Shader shader = new Game.Shader();
        
        shader.Use();
    }

    public static void UseShader()
    {
        Game.Shader shader = new Game.Shader();
        
        shader.Use();
    }
    public void Render(VisualElement drawable)
    {

        _window.RendererQueue.Enqueue(() => RenderTriangle((Triangle) drawable));
        // if (drawable is Triangle)
        // {
        //     RenderTriangle((Triangle)drawable);
        // }
    }
}