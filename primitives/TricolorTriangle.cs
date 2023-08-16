using OpenTK.Graphics.OpenGL4;
using OpenTK3DEngine;

namespace OpenTK3DEngine
{
  public class TricolorTriangle
  {
    // Members
    public int VertexBufferObject;
    public int VertexArrayObject;
    public Shader shader;

    float[] vertices = {
       // positions        // colors
      0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,   // bottom right
     -0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,   // bottom left
      0.0f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f    // top 
    };

    // Constructor
    // @TODO: shader paths are:
    // shaders/default_shader.frag
    // shaders/default_shader.vert

    public TricolorTriangle()
    {
      // Loading shaders
      // 1. vertex, 2. fragment
      shader = new Shader("shaders/attrib_color.vert", "shaders/attrib_color.frag");

      // Loading data to buffers
      this.VertexBufferObject = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, this.VertexBufferObject);

      GL.BufferData(BufferTarget.ArrayBuffer,
          this.vertices.Length * sizeof(float),
          this.vertices,
          BufferUsageHint.StaticDraw);

      // Initializing VAO
      VertexArrayObject = GL.GenVertexArray();

      // Attaching data to shader stuuuuuufffff
      GL.BindVertexArray(VertexArrayObject);
      GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
      GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

      // Setting up Attrib Pointers for shaders
      // Vertices for layout location 0
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);

      // Colors for layout location 1
      GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
      GL.EnableVertexAttribArray(1);

    }

    public void Render()
    {
      shader.Use();
      GL.BindVertexArray(VertexArrayObject);
      GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }
  }
}

