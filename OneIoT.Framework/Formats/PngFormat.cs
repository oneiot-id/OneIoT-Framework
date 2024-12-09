using OneIoT.Framework.Graphics.VisualElements;

namespace OneIoT.Framework.Formats;

public enum PngColor
{
    Grayscale = 0,
    Rgb = 2,
    Indexed = 3,
    GrayscaleAlpha = 4,
    RgbAlpha = 6
}

public enum PngInterlace
{
    NoInterlace = 0,
    Adam = 1
}

public class PngFormat
{
    public Size ImageSize = new Size();

    public PngColor PngColor { get; set; }
    
    public int BitDepth { get; set; }
    
    public const int CompressionMethod = 0;

    public const int FilterMethod = 0;

    public const int InterlaceMethod = 0;
}