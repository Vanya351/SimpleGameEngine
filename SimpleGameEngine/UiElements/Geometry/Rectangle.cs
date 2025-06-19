using OpenTK.Mathematics;
using SimpleGameEngine.Commons;
using SimpleGameEngine.Texturing;

namespace SimpleGameEngine.UiElements.Geometry;

public class Rectangle : UiElement
{
    internal override uint[] Indices => new uint[] { 0, 1, 3, 1, 2, 3 };
    internal override float[,] TextureIndices => new float[,] { { 0f, 1f }, { 1f, 1f }, { 1f, 0f }, { 0f, 0f } };

    protected override void InitializeArrays()
    {
        _position = new float[4, 2];
        _colors = new float[4, 4];
    }

    public Rectangle(float[] position, Anchor anchor, float[] size, Color4 backgroundColor,
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
        : base(backgroundColor, rotationDegrees, visible, zIndex)
    {
        Place(position, anchor, size);
    }

    public Rectangle(float[] position, Anchor anchor, float[] size, Color4[] gradient,
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
        : base(gradient, rotationDegrees, visible, zIndex)
    {
        Place(position, anchor, size);
    }

    public Rectangle(float[] position, Anchor anchor, float[] size, Texture texture,
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
        : base(texture, rotationDegrees, visible, zIndex)
    {
        Place(position, anchor, size);
    }
    
    /// <summary>
    /// places element in new position
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement anchor</param>
    public override void Place(float[] position, Anchor anchor = Anchor.TopLeft)
    {
        float xMax = _position[0, 0], yMax = _position[0, 1], xMin = _position[0, 0], yMin = _position[0, 1];

        for (int i = 1; i < 4; i++)
        {
            if (_position[i, 0] > xMax) xMax = _position[i, 0];
            if (_position[i, 0] < xMin) xMin = _position[i, 0];
            if (_position[i, 1] > yMax) yMax = _position[i, 1];
            if (_position[i, 1] < yMin) yMin = _position[i, 1];
        }
        
        Place(position, anchor, [xMax - xMin, yMax - yMin]);
    }
    
    /// <summary>
    /// places element in new position with new rotation
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement anchor</param>
    /// <param name="rotationDegrees">tilt counterclockwise in degrees</param>
    public override void Place(float[] position, Anchor anchor, float rotationDegrees)
    {
        float xMax = _position[0, 0], yMax = _position[0, 1], xMin = _position[0, 0], yMin = _position[0, 1];

        for (int i = 1; i < 4; i++)
        {
            if (_position[i, 0] > xMax) xMax = _position[i, 0];
            if (_position[i, 0] < xMin) xMin = _position[i, 0];
            if (_position[i, 1] > yMax) yMax = _position[i, 1];
            if (_position[i, 1] < yMin) yMin = _position[i, 1];
        }
        
        Place(position, anchor, [xMax - xMin, yMax - yMin]);
        SetRotation(rotationDegrees);
    }
    
    /// <summary>
    /// places element in new position with new size
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement anchor</param>
    /// <param name="size">new size of element</param>
    public void Place(float[] position, Anchor anchor, float[] size)
    {
        position = AnchorOperations.GetTopLeft(anchor, position, size);
        
        _position[0, 0] = position[0]; _position[0, 1] = position[1];
        position[0] += size[0];
        _position[1, 0] = position[0]; _position[1, 1] = position[1];
        position[1] -= size[1];
        _position[2, 0] = position[0]; _position[2, 1] = position[1];
        position[0] -= size[0];
        _position[3, 0] = position[0]; _position[3, 1] = position[1];
    }
    
    /// <summary>
    /// places element in new position with new size and rotation
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement anchor</param>
    /// <param name="size">new size of element</param>
    /// <param name="rotationDegrees">tilt counterclockwise in degrees</param>
    public void Place(float[] position, Anchor anchor, float[] size, float rotationDegrees)
    {
        position = AnchorOperations.GetTopLeft(anchor, position, size);
        
        _position[0, 0] = position[0]; _position[0, 1] = position[1];
        position[0] += size[0];
        _position[1, 0] = position[0]; _position[1, 1] = position[1];
        position[1] -= size[1];
        _position[2, 0] = position[0]; _position[2, 1] = position[1];
        position[0] -= size[0];
        _position[3, 0] = position[0]; _position[3, 1] = position[1];
        
        SetRotation(rotationDegrees);
    }
    
    /// <summary>
    /// change background color of element to chosen gradient
    /// </summary>
    /// <param name="gradient">4 colors meaning color in each angle of element</param>
    public override void Colorize(Color4[] gradient)
    {
        for (int i = 0; i < gradient.Length * 4; i++)
        {
            switch (i % 4)
            {
                case 0:
                    _colors[i / 4, i % 4] = gradient[i / 4].R;
                    break;
                case 1:
                    _colors[i / 4, i % 4] = gradient[i / 4].G;
                    break;
                case 2:
                    _colors[i / 4, i % 4] = gradient[i / 4].B;
                    break;
                case 3:
                    _colors[i / 4, i % 4] = gradient[i / 4].A;
                    break;
            }
        }
        _backgroundType = Background.Gradient;
    }

    /// <summary>
    /// change background color of element to chosen
    /// </summary>
    /// <param name="color">background color</param>
    public override void Colorize(Color4 color)
    {
        _colors[0, 0] = color.R;
        _colors[0, 1] = color.G;
        _colors[0, 2] = color.B;
        _colors[0, 3] = color.A;
        _backgroundType = Background.Color;
    }
    
    public override void Rescale(float size, Anchor anchor = Anchor.Center)
    {
        throw new NotImplementedException();
    }
}