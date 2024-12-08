using System.Runtime.CompilerServices;
using OneIoT.Framework.Graphics.Shapes;
using OneIoT.Framework.Graphics.VisualElements;
using OpenTK.Mathematics;

namespace OneIoT.Framework.Utils;

public static class VisualElementHelper
{
    public static float[][]  TriangleFromBox(Box box)
    {
        Anchors[] side1 = new Anchors[] { Anchors.TopLeft , Anchors.TopRight, Anchors.BottomLeft};
        Anchors[] side2 = new Anchors[] { Anchors.BottomLeft , Anchors.TopRight, Anchors.BottomRight};

        List<float> v1 = new List<float>();
        List<float> v2 = new List<float>();

        var vertices = box.Points;
        var color = box.Color.BackgroundColor;
        
        for (int i = 0; i < side1.Length; i++)
        {
            //Location
            v1.Add(GetPoint(vertices, side1[i]).X); // X
            v1.Add(GetPoint(vertices, side1[i]).Y); // Y
            v1.Add(0.0f); // Z
            
            //Colour
            v1.Add(color.Red);// R
            v1.Add(color.Green); // G
            v1.Add(color.Blue); // B
            v1.Add(color.Alpha); // A

            //Location
            v2.Add(GetPoint(vertices, side2[i]).X);
            v2.Add(GetPoint(vertices, side2[i]).Y);
            v2.Add(0.0f);
            
            //Colour
            v2.Add(color.Red);// R
            v2.Add(color.Green); // G
            v2.Add(color.Blue); // B
            v2.Add(color.Alpha); // A

        }

        return new float[][] { v1.ToArray(), v2.ToArray()};
    }

    public static float[] GetBoxVertices(Box box)
    {
        var vertices = box.Points;
        var color = box.Color.BackgroundColor;
        
        Anchors[] indices= new Anchors[] { Anchors.TopLeft, Anchors.TopRight, Anchors.BottomLeft, Anchors.BottomRight};
        List<float> v1 = new List<float>();

        for (int i = 0; i < 4; i++)
        {
            //Location
            v1.Add(GetPoint(vertices, indices[i]).X); // X
            v1.Add(GetPoint(vertices, indices[i]).Y); // Y
            v1.Add(0.0f); // Z
            
            //Colour
            v1.Add(color.Red);// R
            v1.Add(color.Green); // G
            v1.Add(color.Blue); // B
            v1.Add(color.Alpha); // A
        }

        return v1.ToArray();
    }

    public static uint ToIndices(this Anchors anchors) => (uint)anchors;

    // public static void Normalize(out float[] vertices, float maxWidth, float maxHeight)
    // {
    //     
    // }

    public static Vector2 GetPoint(Vector2[] point, Anchors side) => point[(int)side];
}