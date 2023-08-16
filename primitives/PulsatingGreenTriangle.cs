using OpenTK.Graphics.OpenGL4;
using OpenTK3DEngine;

namespace OpenTK3DEngine
{
  public class PulsatingGreenTriangle
  {
    // Members
    public int VertexBufferObject;
    public int VertexArrayObject;
    public Shader shader;

    // private float timeValue = 0.0f;

    public float[] vertices = {
      -0.5f, -0.5f, 0.0f, // Bottom-left vertex
       0.5f, -0.5f, 0.0f, // Bottom-right vertex
       0.0f,  0.5f, 0.0f, // Top vertex
    };

    // Constructor
    // @TODO: shader paths are:
    // shaders/default_shader.frag
    // shaders/default_shader.vert

    public PulsatingGreenTriangle()
    {
      // Loading shaders
      // 1. vertex, 2. fragment
      shader = new Shader("shaders/pulsating_green.vert", "shaders/pulsating_green.frag");

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
    public void Render(float dt)
    {
      shader.Use();

      // Pulsating is here!
      // Playing with uniforms. Those are accessed by NAME
      // double timeValue = _timer.Elapsed.TotalSeconds;

      // timeValue += dt;
      float greenValue = (float)Math.Sin(dt) / 2.0f + 0.5f;
      int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");
      GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);

      GL.BindVertexArray(VertexArrayObject);
      GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }

  }
}
