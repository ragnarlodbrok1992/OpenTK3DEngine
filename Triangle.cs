using OpenTK.Graphics.OpenGL4;
using OpenTK3DEngine;

namespace OpenTK3DEngine
{
  public class Triangle
  {
    // Members
    public int VertexBufferObject;
    public int VertexArrayObject;
    public Shader shader;

    public float[] vertices = {
      -0.5f, -0.5f, 0.0f, // Bottom-left vertex
       0.5f, -0.5f, 0.0f, // Bottom-right vertex
       0.0f,  0.5f, 0.0f, // Top vertex
    };

    // Constructor
    // @TODO: shader paths are:
    // shaders/default_shader.frag
    // shaders/default_shader.vert

    public Triangle()
    {
      // Loading shaders
      // 1. vertex, 2. fragment
      shader = new Shader("shaders/default_shader.vert", "shaders/default_shader.frag");

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
      // Attrib Pointer are for actual shaders (only location, color in shader - shaders v 0.1)
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
    }

    // Finally rendering method!
    public void Render()
    {
      shader.Use();
      GL.BindVertexArray(VertexArrayObject);
      GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }

  }

}
