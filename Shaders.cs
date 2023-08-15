// Global imports
using OpenTK.Graphics.OpenGL4;

namespace OpenTK3DEngine
{
  public class Shader : IDisposable 
  {
    // As of OpenTK tutorial, many names for the same shit all around
    // This is a handle to compiled program consisting of both vertex
    // and fragment shader, neatly packed in one class.
    public int Handle;

    // OOP problem - this is not even funny
    // https://www.khronos.org/opengl/wiki/Common_Mistakes#The_Object_Oriented_Language_Problem
    private bool disposedValue = false;

    // Disposing methods
    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        GL.DeleteProgram(Handle);

        disposedValue = true;
      }
    }

    // Destructor
    ~Shader()
    {
      if (disposedValue == false)
      {
        Console.WriteLine("GPU resource leak! Did you forget to call Dispose()?");
      }
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    // Constructor get's paths to vertex and fragment shader
    // since OpenGL requires both of them to show anything on a screen
    public Shader(string vertex_path, string fragment_path)
    {
      // Load sources of shaders
      string VertexShaderSource = File.ReadAllText(vertex_path);
      string FragmentShaderSource = File.ReadAllText(fragment_path);
      int VertexShader = GL.CreateShader(ShaderType.VertexShader);
      GL.ShaderSource(VertexShader, VertexShaderSource);
      int FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
      GL.ShaderSource(FragmentShader, FragmentShaderSource);

      // Compiling shaders - vertex
      GL.CompileShader(VertexShader);
      GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int success);
      if (success == 0)
      {
        string infoLog = GL.GetShaderInfoLog(VertexShader);
        Console.WriteLine(infoLog);
      }

      // Compiling shaders - fragment
      GL.CompileShader(FragmentShader);
      GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out success);
      if (success == 0)
      {
        string infoLog = GL.GetShaderInfoLog(FragmentShader);
        Console.WriteLine(infoLog);
      }

      // Linking program if shaders are well-done
      Handle = GL.CreateProgram();
      GL.AttachShader(Handle, VertexShader);
      GL.AttachShader(Handle, FragmentShader);

      GL.LinkProgram(Handle);

      GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out success);
      if (success == 0)
      {
        string infoLog = GL.GetProgramInfoLog(Handle);
        Console.WriteLine(infoLog);
      }

      // Detaching and deleting shaders
      // When they are compiled and link data is copied into
      // program.
      GL.DetachShader(Handle, VertexShader);
      GL.DetachShader(Handle, FragmentShader);
      GL.DeleteShader(FragmentShader);
      GL.DeleteShader(VertexShader);
    }

    // Non-constructor methods
    public void Use()
    {
      GL.UseProgram(Handle);
    }
  }
}
