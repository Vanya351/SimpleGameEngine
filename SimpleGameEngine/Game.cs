using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SimpleGameEngine.Commons;
using SimpleGameEngine.Shaders;
using SimpleGameEngine.Texturing;
using SimpleGameEngine.UiElements;
using Rectangle = SimpleGameEngine.UiElements.Geometry.Rectangle;

namespace SimpleGameEngine;

public class Game(int width, int height, string title) : GameWindow(GameWindowSettings.Default,
    new NativeWindowSettings()
        { ClientSize = (width, height), Title = title })
{
    private int _vertexBufferObject;
    private int _vertexArrayObject;
    
    private List<float> _vertices = new List<float>();  // note every adding must contain 6 values: x, y, r, g, b, a
                                                        // or 4: x, y, tex_x, tex_y - in case of textures
    private List<uint> _indices = new List<uint>();
    
    public static List<UiElement> UiElements = new List<UiElement>();
    
    private static class Shaders
    {
        public static readonly Shader Base = 
            new Shader("Shaders/baseShader.vert", "Shaders/baseShader.frag");
        
        public static readonly Shader Texture = 
            new Shader("Shaders/texShader.vert", "Shaders/texShader.frag");
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
        
        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        if (UiElements.Count != 0)
        {
            bool wasTexture = false;

            void DrawFilled()
            {
                Shaders.Base.Use();

                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);

                GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 6 * sizeof(float), 2 * sizeof(float));
                GL.EnableVertexAttribArray(1);

                GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * sizeof(float), _vertices.ToArray(), BufferUsageHint.DynamicDraw);
                GL.BindVertexArray(_vertexArrayObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Count * sizeof(uint), _indices.ToArray(), BufferUsageHint.DynamicDraw);

                GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
            }

            foreach (var element in UiElements)
            {
                if (element.BackgroundType == Background.Texture || wasTexture)
                {
                    if (!wasTexture)
                    {
                        DrawFilled();

                        Shaders.Texture.Use();

                        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
                        GL.EnableVertexAttribArray(0);

                        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float),
                            2 * sizeof(float));
                        GL.EnableVertexAttribArray(1);
                    }
                    
                    _vertices.Clear(); _indices.Clear();
                }
                
                uint baseIndex = (uint)(element.BackgroundType == Background.Texture ? 0 : _vertices.Count / 6);

                for (byte i = 0; i < element.Position.GetLength(0); i++)
                {
                    switch (element.BackgroundType)
                    {
                        case Background.Color:
                            _vertices.AddRange([
                                element.Position[i, 0], element.Position[i, 1],
                                element.Colors[0, 0], element.Colors[0, 1], element.Colors[0, 2], element.Colors[0, 3]
                            ]);
                            break;
                        case Background.Gradient:
                            _vertices.AddRange([
                                element.Position[i, 0], element.Position[i, 1],
                                element.Colors[i, 0], element.Colors[i, 1], element.Colors[i, 2], element.Colors[i, 3]
                            ]);
                            break;
                        case Background.Texture:
                            _vertices.AddRange([
                                element.Position[i, 0], element.Position[i, 1], 
                                element.TextureIndices[i, 0], element.TextureIndices[i, 1]
                            ]);
                            break;
                    }
                }

                foreach (uint index in element.Indices)
                {
                    _indices.Add(index + baseIndex);
                }

                if (element.BackgroundType == Background.Texture)
                {
                    element.Texture.Use(TextureUnit.Texture0);
                    
                    GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * sizeof(float), _vertices.ToArray(),
                        BufferUsageHint.DynamicDraw);
                    GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Count * sizeof(uint), _indices.ToArray(),
                        BufferUsageHint.DynamicDraw);
                    GL.BindVertexArray(_vertexArrayObject);
                    
                    GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
                }
                
                wasTexture = (element.BackgroundType == Background.Texture);
            }

            if (!wasTexture)
                DrawFilled();
        }

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
        Shaders.Texture.Dispose();
    }
}