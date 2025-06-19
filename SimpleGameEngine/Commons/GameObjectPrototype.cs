using OpenTK.Mathematics;
using SimpleGameEngine.Texturing;

namespace SimpleGameEngine.Commons;

public abstract class GameObjectPrototype
{
    protected Vector2[] _position = null!;
    internal Vector2[] Position => _position;
    
    protected Color4[] _colors = null!;
    internal Color4[] Colors => _colors;
    
    protected Texture _texture = null!;
    internal Texture Texture => _texture;

    public float Rotation = 0;

    protected Background _backgroundType;
    internal Background BackgroundType => _backgroundType;
    
    protected bool _visible;
    public abstract bool Visible { get; set; }
    
    protected float _zIndex;
    public abstract float ZIndex { get; set; }

    internal abstract uint[] Indices { get; }
    internal abstract Vector2[] TextureIndices { get; }

    protected abstract void InitializeArrays();
    
    /*
     * Note:
     * Ignore warning "virtual member call in constructor"
     * it's mentioned, that most derived version of method must be called
     */
    
    
    protected GameObjectPrototype(Color4 backgroundColor, 
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
    {
        InitializeArrays();
        Colorize(backgroundColor);
        
        Visible = visible;
        ZIndex = zIndex;
        
        this.SetRotation(rotationDegrees);
    }
    
    protected GameObjectPrototype(Color4[] gradient, 
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
    {
        InitializeArrays();
        Colorize(gradient);
        
        Visible = visible;
        ZIndex = zIndex;
        
        this.SetRotation(rotationDegrees);
    }
    
    protected GameObjectPrototype(Texture texture, 
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
    {
        InitializeArrays();
        Colorize(texture);
        
        Visible = visible;
        ZIndex = zIndex;
        
        this.SetRotation(rotationDegrees);
    }

    protected GameObjectPrototype()
    {
        InitializeArrays();
        
        Visible = false;
    }
    
    public abstract void Place(Vector2 position, Anchor anchor = Anchor.TopLeft);
    
    public abstract void Place(Vector2 position, Anchor anchor, float rotationDegrees);

    public abstract void Colorize(Color4[] gradient);

    public abstract void Colorize(Color4 color);
    
    public abstract void Colorize(Texture texture);

    public void Rotate(float degrees)
    {
        Rotation += MathHelper.DegreesToRadians(degrees);
    }

    public void SetRotation(float degrees)
    {
        Rotation = MathHelper.DegreesToRadians(degrees);
    }
    
    public abstract void Rescale(float scale, Anchor anchor = Anchor.Center);
    
    public abstract void Rescale(float xScale = 1, float yScale = 1, Anchor anchor = Anchor.Center);
    
    public abstract void Move(float x = 0, float y = 0);
}