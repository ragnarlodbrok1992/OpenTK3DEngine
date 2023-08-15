// Global imports
using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

// Local imports
using OpenTK3DEngine;

namespace OpenTK3DEngine
{ 
  public class Engine : GameWindow
  {
    // Members of Engine class
    Triangle tutorial_triangle;
    Rectangle tutorial_rectangle;

    // Main of our program - keep it above OOP bullshit
    public static void Main(String[] args)
    {
      // Hello messages
      Console.WriteLine("OpenTK3DEngine - development version");

      // DEBUG - printing out triangle stuff - with our own Util function
      // Triangle tutorial_triangle = new Triangle();
      // Util.printArray(tutorial_triangle.vertices);

      // Array.ForEach(tutorial_triangle.vertices, Console.WriteLine);
      // Console.WriteLine("[{0}]", string.Join(", ", tutorial_triangle.vertices));

      // Starting the engine - requires to run Run() method ;)
      using (Engine engine = new Engine(800, 600, "OpenTK3DEngine - development version"))
      {
        engine.Run();
      }
      
    }

    // Creating instance of this Engine
    public Engine(int width, int height, string title) :
        base(GameWindowSettings.Default, new NativeWindowSettings()
        {
            Size = (width, height),
            Title = title
        })
    /* Local initialization */
    {
      this.tutorial_triangle = new Triangle();
      this.tutorial_rectangle = new Rectangle();
    }

    // Overriding OnLoad method - runs once at the start of the engine
    // I kinda feel like the stuff I would like to do with pure OpenGL in C
    // is already done for me and I just have to build a wrapper on a wrapper on a wrapper...
    // But gotta learn C# somehow, am I right?
    protected override void OnLoad()
    {
      base.OnLoad();

      GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

      // Initialization code goes here
    }

    // Overriding OnUnload method
    protected override void OnUnload()
    {
      base.OnUnload();

      // Disposing of shaders
      // @TODO: should we do it by hand?
      tutorial_triangle.shader.Dispose();

    }

    // Overriding OnUpdateFrame method
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      if (KeyboardState.IsKeyDown(Keys.Escape))
      {
        Close();
      }
    }

    // Overriding OnRenderFrame method
    // I think this one goes after processing input from OnUpdateFrame
    // We can always check OpenTK for that
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit);

      // Code for rendering goes here
      tutorial_triangle.Render();

      SwapBuffers();
    }

    // Overriding OnResize method
    // Just a quality-of-life method that changes viewport when we resize
    // the window. Such a little happy thing <3
    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);

      GL.Viewport(0, 0, e.Width, e.Height);
    }

    // End of Engine class
    }
}
