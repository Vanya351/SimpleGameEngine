using OpenTK.Mathematics;
using SimpleGameEngine.Commons;
using SimpleGameEngine.Texturing;

namespace SimpleGameEngine.UiElements.Geometry;

public class Rectangle : UiElement
{
    internal override uint[] Indices => new uint[] { 0, 1, 3, 1, 2, 3 };
    internal override Vector2[] TextureIndices => new Vector2[] { new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(1f, 0f), new Vector2(0f, 0f) };

    protected override void InitializeArrays()
    {
        _position = new Vector2[4];
        _colors = new Color4[4];
    }

    public Rectangle(Vector2 position, Anchor anchor, Vector2 size, Color4 backgroundColor,
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
        : base(backgroundColor, rotationDegrees, visible, zIndex)
    {
        Place(position, anchor, size);
    }

    public Rectangle(Vector2 position, Anchor anchor, Vector2 size, Color4[] gradient,
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
        : base(gradient, rotationDegrees, visible, zIndex)
    {
        Place(position, anchor, size);
    }

    public Rectangle(Vector2 position, Anchor anchor, Vector2 size, Texture texture,
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
        : base(texture, rotationDegrees, visible, zIndex)
    {
        Place(position, anchor, size);
    }
    
    public Rectangle() : base() { }
    
    
    /// <summary>
    /// places element in new position
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement anchor</param>
    public override void Place(Vector2 position, Anchor anchor = Anchor.TopLeft)
    {
        float xMax = _position[0].X, yMax = _position[0].Y, xMin = _position[0].X, yMin = _position[0].Y;

        for (int i = 1; i < 4; i++)
        {
            if (_position[i].X > xMax) xMax = _position[i].X;
            if (_position[i].X < xMin) xMin = _position[i].X;
            if (_position[i].Y > yMax) yMax = _position[i].Y;
            if (_position[i].Y < yMin) yMin = _position[i].Y;
        }
        
        Place(position, anchor, new Vector2(xMax - xMin, yMax - yMin));
    }
    
    /// <summary>
    /// places element in new position with new rotation
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement anchor</param>
    /// <param name="rotationDegrees">tilt counterclockwise in degrees</param>
    public override void Place(Vector2 position, Anchor anchor, float rotationDegrees)
    {
        Place(position, anchor);
        SetRotation(rotationDegrees);
    }
    
    /// <summary>
    /// places element in new position with new size
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement anchor</param>
    /// <param name="size">new size of element</param>
    public void Place(Vector2 position, Anchor anchor, Vector2 size)
    {
        position = AnchorOperations.GetTopLeft(anchor, position, size);
        
        _position[0].X = position.X; _position[0].Y = position.Y;
        position.X += size.X;
        _position[1].X = position.X; _position[1].Y = position.Y;
        position.Y -= size.Y;
        _position[2].X = position.X; _position[2].Y = position.Y;
        position.X -= size.X;
        _position[3].X = position.X; _position[3].Y = position.Y;
    }
    
    /// <summary>
    /// places element in new position with new size and rotation
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement anchor</param>
    /// <param name="size">new size of element</param>
    /// <param name="rotationDegrees">tilt counterclockwise in degrees</param>
    public void Place(Vector2 position, Anchor anchor, Vector2 size, float rotationDegrees)
    {
        Place(position, anchor, size);
        SetRotation(rotationDegrees);
    }
    
    
    /// <summary>
    /// change background color of element to chosen gradient
    /// </summary>
    /// <param name="gradient">4 colors meaning color in each angle of element</param>
    public override void Colorize(Color4[] gradient)
    {
        if (gradient.Length != 4)
            throw new ArgumentException("Gradient for rectangle must be of length 4.");
        
        _colors = gradient;
        _backgroundType = Background.Gradient;
    }

    /// <summary>
    /// change background color of element to chosen
    /// </summary>
    /// <param name="color">background color</param>
    public override void Colorize(Color4 color)
    {
        _colors[0] = color;
        _backgroundType = Background.Color;
    }
    
    
    public override void Rescale(float scale, Anchor anchor = Anchor.Center)
    {
        Rescale(scale, scale, anchor);
    }

    public override void Rescale(float xScale = 1, float yScale = 1, Anchor anchor = Anchor.Center)
    {
        float xMax = _position[0].X, yMax = _position[0].Y, xMin = _position[0].X, yMin = _position[0].Y;

        for (int i = 1; i < 4; i++)
        {
            if (_position[i].X > xMax) xMax = _position[i].X;
            if (_position[i].X < xMin) xMin = _position[i].X;
            if (_position[i].Y > yMax) yMax = _position[i].Y;
            if (_position[i].Y < yMin) yMin = _position[i].Y;
        }
        
        Place(
            AnchorOperations.GetFromCenter(anchor, new Vector2((xMin + xMax) / 2, (yMin + yMax) / 2), 
                new Vector2(xMax - xMin, yMax - yMin)), anchor,
            new Vector2((xMax - xMin) * xScale, (yMax - yMin) * yScale)
        );
    }

    public override void Move(float x = 0, float y = 0)
    {
        for (byte i = 0; i < 4; i++)
        {
            _position[i].X += x;
            _position[i].Y += y;
        }
    }
}