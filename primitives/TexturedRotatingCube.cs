using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK3DEngine;

namespace OpenTK3DEngine
{
  public class TexturedRotatingCube
  {
    // Members
    public int VertexBufferObject;
    public int VertexArrayObject;
    public int ElementBufferObject;
    public Shader shader;
    public Texture texture;

    // Time stuff
    float totalTime;
    
    // Vertices
    // @TODO moliwa: fill out values for Cube and textures

    // Textured cube good solution
    uint[]  indices  = {
      0,  1,  5,  5,  1,  6,
      1,  2,  6,  6,  2,  7,
      2,  3,  7,  7,  3,  8,
      3,  4,  8,  8,  4,  9,
     10, 11,  0,  0, 11,  1,
      5,  6, 12, 12,  6, 13
    };

    // Good solution for texturing stuff!
    float[] vertices = {
    -1.0f, -1.0f, -1.0f, 0.0f, 0.0f,
     1.0f, -1.0f, -1.0f, 1.0f, 0.0f,
     1.0f,  1.0f, -1.0f, 2.0f, 0.0f,
    -1.0f,  1.0f, -1.0f, 3.0f, 0.0f,
    -1.0f, -1.0f, -1.0f, 4.0f, 0.0f,
    -1.0f, -1.0f,  1.0f, 0.0f, 1.0f,
     1.0f, -1.0f,  1.0f, 1.0f, 1.0f,
     1.0f,  1.0f,  1.0f, 2.0f, 1.0f,
    -1.0f,  1.0f,  1.0f, 3.0f, 1.0f,
    -1.0f, -1.0f,  1.0f, 4.0f, 1.0f,
    -1.0f,  1.0f, -1.0f, 0.0f,-1.0f,
     1.0f,  1.0f, -1.0f, 1.0f,-1.0f,
    -1.0f,  1.0f,  1.0f, 0.0f, 2.0f,
     1.0f,  1.0f,  1.0f, 1.0f, 2.0f
    };

    public TexturedRotatingCube()
    {
      // Loading shaders
      // 1. vertex, 2. fragment
      // Loading any textures that this class needs
      // shaders are the same as for rotating rectangle
      shader = new Shader("shaders/tex_rot_rect.vert", "shaders/tex_rot_rect.frag");
      texture = new Texture("assets/box.png");

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

      this.totalTime += dt;

      // OpenGL important stuff - enable depth test
      // GL.Enable(EnableCap.DepthTest);
      // GL.DepthFunc(DepthFunction.Less);

      Matrix4 rotation_z = Matrix4.CreateRotationZ(this.totalTime); // rotating using delta time on Z axis
      // Matrix4 rotation_x = Matrix4.CreateRotationX(this.totalTime); // rotation using delta time on X axis
      Matrix4 rotation_y = Matrix4.CreateRotationY(this.totalTime); // rotation using delta time on Y axis
      Matrix4 scale = Matrix4.CreateScale(0.5f, 0.5f, 0.5f);
      Matrix4 trans = rotation_z * rotation_y * scale;

      int matrixLocation = GL.GetUniformLocation(shader.Handle, "transform");
      GL.UniformMatrix4(matrixLocation, true, ref trans);

      GL.BindVertexArray(VertexArrayObject);
      GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
    }
  }
}

