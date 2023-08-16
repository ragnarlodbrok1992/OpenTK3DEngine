using OpenTK.Graphics.OpenGL4;
using OpenTK3DEngine;

namespace OpenTK3DEngine
{
  public class TexturedRectangle
  {
    // Members
    public int VertexBufferObject;
    public int VertexArrayObject;
    public int ElementBufferObject;
    public Shader shader;
    public Texture texture;

    // Vertices
    float[] vertices =
    {
        //Position          Texture coordinates
         0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
         0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
        -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
        -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
    };

    uint[] indices = {
      0, 1, 3, // first triangle
      1, 2, 3  // second triangle
    };

    // Constructor
    // @TODO: shader paths are:
    // shaders/default_shader.frag
    // shaders/default_shader.vert

    public TexturedRectangle()
    {
      // Loading shaders
      // 1. vertex, 2. fragment
      shader = new Shader("shaders/tex_rect.vert", "shaders/tex_rect.frag");
      texture = new Texture("assets/box.png");

      // Loading any textures that this class needs

      // Loading data to buffers - VBO
      this.VertexBufferObject = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, this.VertexBufferObject);
      GL.BufferData(BufferTarget.ArrayBuffer,
          this.vertices.Length * sizeof(float),
          this.vertices,
          BufferUsageHint.StaticDraw);

      // Initializating VAO
      VertexArrayObject = GL.GenVertexArray();

      // Attaching data to shaders
      GL.BindVertexArray(VertexArrayObject);
      GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
      GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

      // EBO goes here
      this.ElementBufferObject = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.ElementBufferObject);
      GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

      // Setting up Attrib Pointers for shaders
      // Vertices for layout location 0
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);

      int texCoordLocation = shader.GetAttribLocation("aTexCoord");
      GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
      GL.EnableVertexAttribArray(texCoordLocation);

    }

    public void Render()
    {
      shader.Use();
      texture.Use();
      GL.BindVertexArray(VertexArrayObject);
      GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
    }
  }
}
