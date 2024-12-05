namespace OneIoT.Framework.Graphics.Windowing;

public interface IWindow
{
    public int FullscreenWidth { get; set; }
    public int FullscreenHeight { get; set; }
    public int ScreenWidth { get; set; }
    public int ScreenHeight { get; set; }
}