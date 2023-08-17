using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK3DEngine;

namespace OpenTK3DEngine
{
  public class TexturedRotatingRectangle
  {
    // Members
    public int VertexBufferObject;
    public int VertexArrayObject;
    public int ElementBufferObject;
    public Shader shader;
    public Texture texture;

    float totalTime;

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


    public TexturedRotatingRectangle()
    {
      // Loading shaders
      // 1. vertex, 2. fragment
      shader = new Shader("shaders/tex_rot_rect.vert", "shaders/tex_rot_rect.frag");
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

    public void Render(float dt)
    {
      shader.Use();
      texture.Use();

      // Playing with uniforms - matrices
      
      this.totalTime += dt;
      // float rotation_radians = (float) Math.Sin(totalTime);
      Matrix4 rotation = Matrix4.CreateRotationZ(this.totalTime); // rotating using delta time
      Matrix4 scale = Matrix4.CreateScale(0.5f, 0.5f, 0.5f);
      Matrix4 trans = rotation * scale;

      int matrixLocation = GL.GetUniformLocation(shader.Handle, "transform");
      GL.UniformMatrix4(matrixLocation, true, ref trans);

      GL.BindVertexArray(VertexArrayObject);
      GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
    }
  }
}
