// Global imports
using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

// Local imports
using OpenTK3DEngine;

namespace OpenTK3DEngine
{ 
  public class Engine : GameWindow
  {
    // Members textured cubes
    // @TODO moliwa: move to container
    TexturedCube tex_cube;

    double totalTime = 0.0f;
    bool firstMove = true;
    // bool firstClick = false;

    // Main of our program - keep it above OOP bullshit
    public static void Main(String[] args)
    {
      // Hello messages
      Console.WriteLine(GLOBALS.ENGINE_TITLE_BAR);

      // Starting the engine - requires to run Run() method ;)
      using (Engine engine = new Engine(
            GLOBALS.ENGINE_WIDTH,
            GLOBALS.ENGINE_HEIGHT,
            GLOBALS.ENGINE_TITLE_BAR))
      {
        // Before running some debug info or something

        // Tutorial fun
        int nrAttributes = 0;
        GL.GetInteger(GetPName.MaxVertexAttribs, out nrAttributes);
        Console.WriteLine("OpenGL properties");
        Console.WriteLine("Vertex attributes supported: " + nrAttributes);

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
      tex_cube = new TexturedCube("assets/box.png");
    }

    // Overriding OnLoad method - runs once at the start of the engine
    // I kinda feel like the stuff I would like to do with pure OpenGL in C
    // is already done for me and I just have to build a wrapper on a wrapper on a wrapper...
    // But gotta learn C# somehow, am I right?
    protected override void OnLoad()
    {
      base.OnLoad();

      GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
      GL.Enable(EnableCap.DepthTest);

      // Initialization code goes here
    }

    // Overriding OnUnload method
    protected override void OnUnload()
    {
      base.OnUnload();

      // Disposing of shaders
      tex_cube.shader.Dispose();
    }

    // Overriding OnUpdateFrame method
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      // Not focused - not processing input
      if (!IsFocused)
      {
        return;
      } else
      {
        // Processing input
        // Closing engine
        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
          Close();
        }

        // Camera movements
        if (KeyboardState.IsKeyDown(Keys.W))
        {
          Camera.position += Camera.front * Camera.speed * (float) e.Time;
        }
        if (KeyboardState.IsKeyDown(Keys.S))
        {
          Camera.position -= Camera.front * Camera.speed * (float) e.Time;
        }
        if (KeyboardState.IsKeyDown(Keys.A))
        {
          Camera.position -= Vector3.Normalize(Vector3.Cross(Camera.front, Camera.up)) * Camera.speed * (float) e.Time;
        }
        if (KeyboardState.IsKeyDown(Keys.D))
        {
          Camera.position += Vector3.Normalize(Vector3.Cross(Camera.front, Camera.up)) * Camera.speed * (float) e.Time;
        }
        if (KeyboardState.IsKeyDown(Keys.Space))
        {
          Camera.position += Camera.up * Camera.speed * (float) e.Time;
        }
        if (KeyboardState.IsKeyDown(Keys.LeftShift))
        {
          Camera.position -= Camera.up * Camera.speed * (float) e.Time;
        }

        // Camera rotation
        // Only rotate camera while you have left mouse button held
        if (MouseState[MouseButton.Left])
        {
          if (firstMove)
          {
            var lastPos = new Vector2(MouseState.X, MouseState.Y);
            firstMove = false;
          }
          else
          {
            // TODO - implement rotation <3
          }
        }
      }

    }

    // Overriding OnRenderFrame method
    // I think this one goes after processing input from OnUpdateFrame
    // We can always check OpenTK for that
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      totalTime += e.Time;

      // Override VIEW matrix for shaders with stuff that we have in camera
      GLOBALS.VIEW = Matrix4.LookAt(Camera.position, Camera.position + Camera.front, Camera.up);

      // Render objects here
      tex_cube.Render((float) e.Time);

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
