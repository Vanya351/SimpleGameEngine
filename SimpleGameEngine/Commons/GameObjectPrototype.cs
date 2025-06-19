using OpenTK.Mathematics;
using SimpleGameEngine.Texturing;

namespace SimpleGameEngine.Commons;

public abstract class GameObjectPrototype
{
    protected float[,] _position = new float[4, 2];
    internal float[,] Position => _position;
    
    protected float[,] _colors = new float[4, 4];
    internal float[,] Colors => _colors;
    
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
    internal abstract float[,] TextureIndices { get; }
    
    
    public GameObjectPrototype(float[] position, Anchor anchor, float[] size, Color4 backgroundColor, 
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
    {
        Place(position, anchor, size);
        Colorize(backgroundColor);
        
        Visible = visible;
        ZIndex = zIndex;
        
        this.SetRotation(rotationDegrees);
    }
    
    public GameObjectPrototype(float[] position, Anchor anchor, float[] size, Color4[] gradient, 
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
    {
        Place(position, anchor, size);
        Colorize(gradient);
        
        Visible = visible;
        ZIndex = zIndex;
        
        this.SetRotation(rotationDegrees);
    }
    
    public GameObjectPrototype(float[] position, Anchor anchor, float[] size, Texture texture, 
        float rotationDegrees = 0, bool visible = true, float zIndex = 0)
    {
        Place(position, anchor, size);
        Colorize(texture);
        
        Visible = visible;
        ZIndex = zIndex;
        
        this.SetRotation(rotationDegrees);
    }
    
    public abstract void Place(float[] position, Anchor anchor);
    
    public abstract void Place(float[] position, Anchor anchor, float rotationDegrees);

    public abstract void Place(float[] position, Anchor anchor, float[] size);
    
    public abstract void Place(float[] position, Anchor anchor, float[] size, float rotationDegrees);

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
    
    public abstract void Rescale(float size, Anchor anchor = Anchor.Center);
}