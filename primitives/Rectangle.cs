using OpenTK.Graphics.OpenGL4;
using OpenTK3DEngine;

namespace OpenTK3DEngine
{
  public class Rectangle
  {
    // Members
    public int VertexBufferObject;
    public int VertexArrayObject;
    public int ElementBufferObject;
    public Shader shader;

    // Vertices
    float[] vertices = {
       0.5f,  0.5f, 0.0f, // top right
       0.5f, -0.5f, 0.0f, // bottom right
      -0.5f, -0.5f, 0.0f, // bottom left
      -0.5f,  0.5f, 0.0f  // top left
    };

    uint[] indices = {
      0, 1, 3, // first triangle
      1, 2, 3  // second triangle
    };

    // Constructor
    // @TODO: shader paths are:
    // shaders/default_shader.frag
    // shaders/default_shader.vert

    public Rectangle()
    {
      // Loading shaders
      // 1. vertex, 2. fragment
      shader = new Shader("shaders/default_shader.vert", "shaders/rectangle_shader.frag");

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
      GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

      // Should we bind EBO here?
      this.ElementBufferObject = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.ElementBufferObject);
      GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

      // Attrib Pointer are for actual shaders (only location, color in shader - shaders v 0.1)
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
    }

    // Finally rendering method!
    public void Render()
    {
      shader.Use();
      GL.BindVertexArray(VertexArrayObject);
      // GL.DrawArrays(PrimitiveType.Triangles, 0, 3); // Old stuff from triangle
      GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
    }

  }

}
