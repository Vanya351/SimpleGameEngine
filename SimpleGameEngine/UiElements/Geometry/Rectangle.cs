using OpenTK.Mathematics;
using SimpleGameEngine.Commons;
using SimpleGameEngine.Texturing;

namespace SimpleGameEngine.UiElements.Geometry;

public class Rectangle : UiElement
{
    internal override uint[] Indices => new uint[] { 0, 1, 3, 1, 2, 3 };
    internal override float[,] TextureIndices => new float[,] { { 0f, 1f }, { 1f, 1f }, { 1f, 0f }, { 0f, 0f } };

    public Rectangle(float[] position, Anchor anchor, float[] size, Color4 backgroundColor,
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
        : base(position, anchor, size, backgroundColor, rotationDegrees, visible, zIndex) { }
    
    public Rectangle(float[] position, Anchor anchor, float[] size, Color4[] gradient,
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
        : base(position, anchor, size, gradient, rotationDegrees, visible, zIndex) { }
    
    public Rectangle(float[] position, Anchor anchor, float[] size, Texture texture,
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
        : base(position, anchor, size, texture, rotationDegrees, visible, zIndex) { }
    
    public override void Rescale(float size, Anchor anchor = Anchor.Center)
    {
        throw new NotImplementedException();
    }
}