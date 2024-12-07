using System.Runtime.CompilerServices;
using OneIoT.Framework.Graphics.Shapes;
using OneIoT.Framework.Graphics.VisualElements;
using OpenTK.Mathematics;

namespace OneIoT.Framework.Utils;

public class VisualElementHelper
{
    public static float[][]  TriangleFromBox(Vector2[] vertices)
    {
        Anchors[] side1 = new Anchors[] { Anchors.TopLeft , Anchors.TopRight, Anchors.BottomLeft};
        Anchors[] side2 = new Anchors[] { Anchors.BottomLeft , Anchors.TopRight, Anchors.BottomRight};

        List<float> v1 = new List<float>();
        List<float> v2 = new List<float>();

        for (int i = 0; i < side1.Length; i++)
        {
            v1.Add(GetPoint(vertices, side1[i]).X);
            v1.Add(GetPoint(vertices, side1[i]).Y);
            v1.Add(0.0f);
            
            v2.Add(GetPoint(vertices, side2[i]).X);
            v2.Add(GetPoint(vertices, side2[i]).Y);
            v2.Add(0.0f);
        }

        return new float[][] { v1.ToArray(), v2.ToArray()};
    }

    // public static void Normalize(out float[] vertices, float maxWidth, float maxHeight)
    // {
    //     
    // }

    public static Vector2 GetPoint(Vector2[] point, Anchors side) => point[(int)side];
}