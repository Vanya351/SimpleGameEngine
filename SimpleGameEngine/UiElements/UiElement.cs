using System.Drawing;
using OpenTK.Mathematics;
using SimpleGameEngine.Commons;
using SimpleGameEngine.Texturing;

namespace SimpleGameEngine.UiElements;

public abstract class UiElement : GameObjectPrototype
{
    public override bool Visible
    {
        get => _visible;

        set
        {
            _visible = value;
            if (Visible)
            {
                Game.UiElements.Add(this);
                Game.UiElements.Sort((x,y) => x.ZIndex.CompareTo(y.ZIndex));
            }
            else if (Game.UiElements.Contains(this))
            {
                Game.UiElements.Remove(this);
            }
        }
    }
    
    public override float ZIndex
    {
        get => _zIndex;
        
        set
        {
            _zIndex = value;
            if (Visible)
                Game.UiElements.Sort((x,y) => x.ZIndex.CompareTo(y.ZIndex));
        }
    }

    /// <summary>
    /// Creates an ui element with one color filled background
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement mode</param>
    /// <param name="size">size of element</param>
    /// <param name="backgroundColor">background color with which element be filled</param>
    /// <param name="visible">visibility of element</param>
    /// <param name="zIndex">element height index</param>
    public UiElement(float[] position, Anchor anchor, float[] size, Color4 backgroundColor, bool visible = true, float zIndex = 0)
        : base(position, anchor, size, backgroundColor, visible) { }
    
    /// <summary>
    /// Creates an ui element with gradient background
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement mode</param>
    /// <param name="size">size of element</param>
    /// <param name="gradient">4 colors meaning color in each angle of element</param>
    /// <param name="visible">visibility of element</param>
    /// <param name="zIndex">element height index</param>
    public UiElement(float[] position, Anchor anchor, float[] size, Color4[] gradient, bool visible = true, float zIndex = 0)
        : base(position, anchor, size, gradient, visible) { }
    
    /// <summary>
    /// Creates an ui element with texture background
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement mode</param>
    /// <param name="size">size of element</param>
    /// <param name="texture">texture</param>
    /// <param name="visible">visibility of element</param>
    /// <param name="zIndex">element height index</param>
    public UiElement(float[] position, Anchor anchor, float[] size, Texture texture, bool visible = true, float zIndex = 0)
        : base(position, anchor, size, texture, visible) { }
    
    /// <summary>
    /// places element in new position
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement anchor</param>
    public override void Place(float[] position, Anchor anchor)
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
    /// places element in new position with new size
    /// </summary>
    /// <param name="position">placement coordinates</param>
    /// <param name="anchor">placement anchor</param>
    /// <param name="size">new size of element</param>
    public override void Place(float[] position, Anchor anchor, float[] size)
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
    
    /// <summary>
    /// change background of element to chosen texture
    /// </summary>
    /// <param name="texture">texture</param>
    public override void Colorize(Texture texture)
    {
        _texture = texture;
        _backgroundType = Background.Texture;
    }
}