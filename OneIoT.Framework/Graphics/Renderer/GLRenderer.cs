using System.Diagnostics;
// using System.Numerics;
using OneIoT.Framework.Graphics.Shapes;
using OneIoT.Framework.Graphics.VisualElements;
using OneIoT.Framework.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Window = OneIoT.Framework.Graphics.Windowing.Window;

namespace OneIoT.Framework.Graphics.Renderer;

// ReSharper disable once InconsistentNaming
public class GLRenderer
{
    private readonly Window _window;

    Shader _shader = new Shader();
    
    private List<VisualElement> _renderedElements = new List<VisualElement>();
    
    private Queue<IVisualElement> _renderQueue = new Queue<IVisualElement>();

    public GLRenderer(Window window)
    {
        _window = window;
    }

    public GLRenderer()
    {
    }

    public List<VisualElement> GetRenderedElements => _renderedElements;

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

    public void AddToRenderQueue(IVisualElement visualElement)
    {
        _renderQueue.Enqueue(visualElement);
    }

    public void Render()
    {
        if (_renderQueue.Count == 0) return;
        
        _renderedElements.Clear();
        
        while (_renderQueue.Count > 0)
        {
            var element = _renderQueue.Dequeue();
            
            if (element.Visible == false)
                continue;
            
            _renderedElements.Add((VisualElement) element);
            RenderElement(element);
        }
        
        _shader.Use();
        _shader.Dispose();
        _window.SwapBuffers();
    }

    public void RenderElement(IVisualElement element)
    {
            switch (element)
            {
                case Triangle:
                    DrawTriangle((Triangle) element);
                    break;
                case Box:
                    DrawBox((Box) element);
                    break;
        }
    }
    
    

    /// <summary>
    /// In future this will handle the transformation such as rotation, scaling, etc
    /// </summary>
    private void HandleTransformation(Transform transform)
    {
        
    }

    /// <summary>
    /// Draw with vertices and triangle
    /// </summary>
    private void Draw(float[] vertices, uint[]? indices = null)
    {
        bool normalized = vertices.All(v => v <= 1 && v >= -1);

        if (!normalized)
        {
            for (int i = 0; i < vertices.Length; i += 7 )
            {
                vertices[i] = CoordinateMapper.NormalizeX(vertices[i], _window.Size.Width);
                vertices[i + 1] = CoordinateMapper.NormalizeY(vertices[i + 1], _window.Size.Height);
            }
        }
        
        int vbo = GL.GenBuffer();
        int vao = GL.GenVertexArray();
        int ebo = GL.GenBuffer();
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        
        GL.BindVertexArray(vao);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        if (indices != null)
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }
        else
        {
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }   
        
    }

    private void DrawBox(Box box)
    {
        float[] vertices1 = VisualElementHelper.GetBoxVertices(box);
        uint[] indices = { 
            0, 1, 2,   
            2, 1, 3   
        };
        Draw(vertices1, indices);

        foreach (var child in box.Children.Child)
        {
            RenderElement(child);
        }
    }
    
    /// <summary>
    /// This will render a triangle object
    /// </summary>
    /// <param name="triangle"></param>
    private void DrawTriangle(Triangle triangle)
    {
        float[] vertices = new float[9];

        // Calculate the screen center in normalized coordinatesvar
        var parent = triangle.Parent ?? _window;
        
        var parentCenterPoint = triangle.CenterPoint;

        // Calculate the triangle's centroid offsets
        float centroidX = triangle.Size.Width / 2f;
        float centroidY = triangle.Size.Height / 2f;

        // Calculate the three vertices of the triangle
        Vector2 p1 = new Vector2(parentCenterPoint.X - centroidX, parentCenterPoint.Y + centroidY); // Bottom-left
        Vector2 p2 = new Vector2(parentCenterPoint.X + centroidX, parentCenterPoint.Y + centroidY); // Bottom-right
        Vector2 p3 = new Vector2(parentCenterPoint.X, parentCenterPoint.Y - centroidY); // Top

        p1 = new Vector2(CoordinateMapper.NormalizeX(p1.X, parent.Size.Width),
            CoordinateMapper.NormalizeY(p1.Y, parent.Size.Height));

        p2 = new Vector2(CoordinateMapper.NormalizeX(p2.X, parent.Size.Width),
            CoordinateMapper.NormalizeY(p2.Y, parent.Size.Height));

        p3 = new Vector2(CoordinateMapper.NormalizeX(p3.X, parent.Size.Width),
            CoordinateMapper.NormalizeY(p3.Y, parent.Size.Height));

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

        int vbo = GL.GenBuffer();
        int vao = GL.GenVertexArray();

        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.BindVertexArray(vao);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

        GL.EnableVertexAttribArray(0);

        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        
        //Make a logic on hover
        //Mouse x is 

        
        //if has children render
        if (triangle.Children.Child.Count > 0)
        {
            // RenderElement(triangle.Children.Child);
        }
    }




    // public static void RenderTri()
    // {
    //     float[] vertices2 = {
    //         -0.5f, -0.5f, 0.0f, //Bottom-left vertex
    //         0.5f, -0.5f, 0.0f, //Bottom-right vertex
    //         0.0f,  0.5f, 0.0f  //Top vertex
    //     };
    //     
    //     int vbo = GL.GenBuffer();
    //     int vao = GL.GenVertexArray();
    //     
    //     GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
    //     GL.BufferData(BufferTarget.ArrayBuffer, vertices2.Length * sizeof(float), vertices2, BufferUsageHint.StaticDraw);
    //     
    //     GL.BindVertexArray(vao);
    //     GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
    //     
    //     GL.EnableVertexAttribArray(0);
    //     
    //     GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
    //
    //     Game.Shader shader = new Game.Shader();
    //     
    //     shader.Use();
    // }
    //
    // public static void UseShader()
    // {
    //     Game.Shader shader = new Game.Shader();
    //     
    //     shader.Use();
    // }
}