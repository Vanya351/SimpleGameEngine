using System.Drawing;
using OpenTK.Mathematics;
using SimpleGameEngine.Commons;
using SimpleGameEngine.Texturing;

namespace SimpleGameEngine.UiElements;

public abstract class UiElement : GameObjectPrototype
{
    /// <summary>
    /// Is object visible at screen
    /// </summary>
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
    
    /// <summary>
    /// "height" above other elements
    /// </summary>
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
    /// <param name="rotationDegrees">tilt counterclockwise in degrees</param>
    /// <param name="backgroundColor">background color with which element be filled</param>
    /// <param name="visible">visibility of element</param>
    /// <param name="zIndex">element height index</param>
    protected UiElement(Color4 backgroundColor,
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
        : base(backgroundColor, rotationDegrees, visible, zIndex) { }
    
    /// <summary>
    /// Creates an ui element with gradient background
    /// </summary>
    /// <param name="rotationDegrees">tilt counterclockwise in degrees</param>
    /// <param name="gradient">4 colors meaning color in each angle of element</param>
    /// <param name="visible">visibility of element</param>
    /// <param name="zIndex">element height index</param>
    protected UiElement(Color4[] gradient,
        float rotationDegrees, bool visible = true, float zIndex = 0)
        : base(gradient, rotationDegrees, visible, zIndex) { }
    
    /// <summary>
    /// Creates an ui element with texture background
    /// </summary>
    /// <param name="rotationDegrees">tilt counterclockwise in degrees</param>
    /// <param name="texture">texture</param>
    /// <param name="visible">visibility of element</param>
    /// <param name="zIndex">element height index</param>
    protected UiElement(Texture texture,
        float rotationDegrees, bool visible = true, float zIndex = 0)
        : base(texture, rotationDegrees, visible, zIndex) { }
    
    
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