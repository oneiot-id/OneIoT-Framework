using OpenTK.Windowing.GraphicsLibraryFramework;
using Window = OneIoT.Framework.Graphics.Windowing.Window;

namespace OneIoT.Framework.Events;

public class Event
{
    private Window _window;

    public Event(Window window)
    {
        _window = window;
    }
    
    public void HandleMouseDown()
    {
        var mX = _window.MousePosition.X;
        var mY = _window.MousePosition.Y;
        
        foreach (var element in _window.GlRenderer.GetRenderedElements)
        {
            /*
             * Hovering
             */
            //Refer to this but improve this like it can read a triangle not a box
            //For now we use box for hitbox
            var halfWidth = element.Size.Width / 2f;
            var left = element.CenterPoint.X - halfWidth;
            var right = element.CenterPoint.X + halfWidth;
            var top = element.CenterPoint.Y - halfWidth;
            var bottom = element.CenterPoint.Y + halfWidth;

            if (mY >= top && mY <= bottom && mX >= left && mX <= right)
            {
                if (_window.MouseState.IsButtonPressed(MouseButton.Button1))
                {
                    Console.WriteLine($"mouse clicked handled by {element.Name} ");
                    element.OnClick();
                }
                else
                {
                    element.OnClickExit();
                }
            }
            else
            {
                element.OnMouseExit();
            }

        }
    }
}