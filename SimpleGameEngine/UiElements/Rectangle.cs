using OpenTK.Mathematics;
using SimpleGameEngine.Commons;

namespace SimpleGameEngine.UiElements;

public class Rectangle : UiElement
{
    public Rectangle(float[] position, Anchor anchor, float[] size, Color4 backgroundColor, bool visible = true, float zIndex = 0)
        : base(position, anchor, size, backgroundColor, visible, zIndex) { }
    
    public Rectangle(float[] position, Anchor anchor, float[] size, Color4[] gradient, bool visible = true, float zIndex = 0)
        : base(position, anchor, size, gradient, visible, zIndex) { }
}