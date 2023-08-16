using OpenTK.Graphics.OpenGL4;
using OpenTK3DEngine;

namespace OpenTK3DEngine
{
  public class TricolorTriangle
  {
    // Members

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
  }
}

