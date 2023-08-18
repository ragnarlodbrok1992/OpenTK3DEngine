using System;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace OpenTK3DEngine
{
  public class Texture
  {
    // Members
    public int Handle;
    public string filePath;

    // Constructor
    public Texture(string filePath)
    {
      Handle = GL.GenTexture();
      this.filePath = filePath;

      // Loading texture from OpenTK tutorial
      GL.ActiveTexture(TextureUnit.Texture0);
      GL.BindTexture(TextureTarget.Texture2D, Handle);

      // Some global texture setup
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);


      StbImage.stbi_set_flip_vertically_on_load(1);
      ImageResult image = ImageResult.FromStream(File.OpenRead(this.filePath), ColorComponents.RedGreenBlueAlpha);
      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
      // Generating mipmaps
      // GL.GenerateMipmaps();
    }

    public void Use()
    {
      GL.ActiveTexture(TextureUnit.Texture0);
      GL.BindTexture(TextureTarget.Texture2D, Handle);
    }
  }
}
