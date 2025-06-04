using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SimpleGameEngine.Commons;
using SimpleGameEngine.UiElements;

namespace SimpleGameEngine;

public class Game(int width, int height, string title) : GameWindow(GameWindowSettings.Default,
    new NativeWindowSettings()
        { ClientSize = (width, height), Title = title })
{
    private int _vertexBufferObject;
    private int _vertexArrayObject;
    
    private List<float> _vertices = new List<float>();  // note every adding should contain 7 values: x, y, r, g, b, a
    private List<uint> _indices = new List<uint>();
    
    public static List<UiElement> UiElements = new List<UiElement>();
    
    private static class Shaders
    {
        public static readonly Shader Base = 
            new Shader("Shaders/baseShader.vert", "Shaders/baseShader.frag");
    }
    
    protected override void OnLoad()
    {
        base.OnLoad();
        
        GL.ClearColor(EngineSettings.BackgroundColor);
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        
        _vertexBufferObject = GL.GenBuffer();
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        
        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject);
        
        int elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 6 * sizeof(float), 2 * sizeof(float));
        GL.EnableVertexAttribArray(1);
    }
    
    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        
        if (EngineSettings.ShowFps)
            Console.WriteLine(1 / UpdateTime);  // fps meter TODO: rebase output into window instead of console
        
        
        // if (KeyboardState.IsKeyDown(Keys.Escape))
        // {
        //     Close();
        // }
    }
    
    
    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        
        _vertices.Clear(); _indices.Clear();
        uint baseIndex;
        foreach (var element in UiElements)
        {
            baseIndex = (uint)_vertices.Count / 6;
            
            for (byte i = 0; i < 4; i++)
            {
                if (element.BackgroundType == Background.Color)
                {
                    _vertices.AddRange([element.Position[i, 0], element.Position[i, 1],
                        element.Colors[0, 0], element.Colors[0, 1], element.Colors[0, 2], element.Colors[0, 3]]);
                }
                else if (element.BackgroundType == Background.Gradient)
                {
                    _vertices.AddRange([element.Position[i, 0], element.Position[i, 1],
                        element.Colors[i, 0], element.Colors[i, 1], element.Colors[i, 2], element.Colors[i, 3]]);
                }
            }
            
            _indices.AddRange([baseIndex, baseIndex + 1, baseIndex + 3, 
                baseIndex + 1, baseIndex + 2, baseIndex + 3]);
        }

        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        Shaders.Base.Use();
        
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * sizeof(float), _vertices.ToArray(), 
            BufferUsageHint.DynamicDraw);
        GL.BindVertexArray(_vertexArrayObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Count * sizeof(uint), _indices.ToArray(),
            BufferUsageHint.DynamicDraw);

        GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);

        SwapBuffers();
    }
    
    
    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }


    protected override void OnUnload()
    {
        base.OnUnload();
        
        Shaders.Base.Dispose();
    }
}