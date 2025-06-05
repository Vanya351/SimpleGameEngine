using OpenTK.Mathematics;

namespace SimpleGameEngine.Commons;

public abstract class GameObjectPrototype
{
    protected float[,] _position = new float[4, 2];
    public float[,] Position => _position;
    
    protected float[,] _colors = new float[4, 4];
    public float[,] Colors => _colors;

    protected Background _backgroundType;
    public Background BackgroundType => _backgroundType;
    
    protected bool _visible;
    public abstract bool Visible { get; set; }
    
    protected float _zIndex;
    public abstract float ZIndex { get; set; }
    
    
    public GameObjectPrototype(float[] position, Anchor anchor, float[] size, Color4 backgroundColor, 
        bool visible = true, float zIndex = 0)
    {
        Place(position, anchor, size);
        Colorize(backgroundColor);
        
        Visible = visible;
        ZIndex = zIndex;
    }
    
    public GameObjectPrototype(float[] position, Anchor anchor, float[] size, Color4[] gradient, 
        bool visible = true, float zIndex = 0)
    {
        Place(position, anchor, size);
        Colorize(gradient);
        
        Visible = visible;
        ZIndex = zIndex;
    }
    
    
    public abstract void Place(float[] position, Anchor anchor);

    public abstract void Place(float[] position, Anchor anchor, float[] size);


    public abstract void Colorize(Color4[] gradient);

    public abstract void Colorize(Color4 color);
}