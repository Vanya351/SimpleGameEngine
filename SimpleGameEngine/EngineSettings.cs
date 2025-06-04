using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace SimpleGameEngine;

public static class EngineSettings
{
    private static Color4 _backgroundColor = Color4.White;
    public static Color4 BackgroundColor
    {
        get => _backgroundColor;

        set
        {
            _backgroundColor = value;
            GL.ClearColor(value.R, value.G, value.B, value.A);
        }
    }
    
    public static bool ShowFps = false;
}