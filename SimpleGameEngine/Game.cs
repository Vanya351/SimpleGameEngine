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
        
        // test data below TODO: remove from final version
        Texture texture = new Texture("tex.png");
        Rectangle rectangle1 = new Rectangle(new Vector2(-0.2f, 0.2f), Anchor.TopLeft, new Vector2(0.5f, 0.5f), Color4.Green);
        Rectangle rectangle2 = new Rectangle(new Vector2(0.4f, 0.2f), Anchor.TopLeft, new Vector2(0.5f, 0.5f), Color4.Orange);
        Rectangle rectangle3 = new Rectangle(new Vector2(0.4f, 0.2f), Anchor.TopLeft, new Vector2(0.5f, 0.5f), texture);
        rectangle3.Rescale(0.5f, 0.8f, Anchor.Left);
        //EngineSettings.ShowFps = true;
        //TODO: there is a strange bug where colored rectangles not shown where last render with texture
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

                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 22 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);

                GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 22 * sizeof(float), 2 * sizeof(float));
                GL.EnableVertexAttribArray(1);
                
                GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, 22 * sizeof(float), 6 * sizeof(float));
                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, 22 * sizeof(float), 10 * sizeof(float));
                GL.EnableVertexAttribArray(3);
                GL.VertexAttribPointer(4, 4, VertexAttribPointerType.Float, false, 22 * sizeof(float), 14 * sizeof(float));
                GL.EnableVertexAttribArray(4);
                GL.VertexAttribPointer(5, 4, VertexAttribPointerType.Float, false, 22 * sizeof(float), 18 * sizeof(float));
                GL.EnableVertexAttribArray(5);

                GL.BindVertexArray(_vertexArrayObject);
                GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * sizeof(float), _vertices.ToArray(), BufferUsageHint.DynamicDraw);
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
                
                uint baseIndex = (uint)(element.BackgroundType == Background.Texture ? 0 : _vertices.Count / 22);
                Matrix4 trans = Matrix4.CreateRotationZ(element.Rotation).Transposed();

                for (byte i = 0; i < element.Position.GetLength(0); i++)
                {
                    switch (element.BackgroundType)
                    {
                        case Background.Color:
                            _vertices.AddRange([
                                element.Position[i].X, element.Position[i].Y,
                                element.Colors[0].R, element.Colors[0].G, element.Colors[0].B, element.Colors[0].A,
                                trans.M11, trans.M12, trans.M13, trans.M14,
                                trans.M21, trans.M22, trans.M23, trans.M24,
                                trans.M31, trans.M32, trans.M33, trans.M34,
                                trans.M41, trans.M42, trans.M43, trans.M44
                            ]);
                            break;
                        case Background.Gradient:
                            _vertices.AddRange([
                                element.Position[i].X, element.Position[i].Y,
                                element.Colors[i].R, element.Colors[i].G, element.Colors[i].B, element.Colors[i].A,
                                trans.M11, trans.M12, trans.M13, trans.M14,
                                trans.M21, trans.M22, trans.M23, trans.M24,
                                trans.M31, trans.M32, trans.M33, trans.M34,
                                trans.M41, trans.M42, trans.M43, trans.M44
                            ]);
                            break;
                        case Background.Texture:
                            _vertices.AddRange([
                                element.Position[i].X, element.Position[i].Y, 
                                element.TextureIndices[i].X, element.TextureIndices[i].Y
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
                    
                    int location = GL.GetUniformLocation(Shaders.Texture.Handle, "transform");
                    GL.UniformMatrix4(location, false, ref trans);
                    
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