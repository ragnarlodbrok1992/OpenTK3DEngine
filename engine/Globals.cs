// Global variables for engine
// Anything that should be acccessable throught lifecycle of engine
// (and anything that is considered global or static (in C# everything has to be in class))
// should go here.
// Some stuff is accessible by some objects - mostly modifing it, since reading is up to anyone
// in this engine.
using OpenTK.Mathematics;

namespace OpenTK3DEngine
{
  // Let's not use this not good capitalization ideas in C#
  // it was made by summer interns and is not that good
  public static class GLOBALS
  {
    // Engine variables
    public const int ENGINE_WIDTH  = 800;
    public const int ENGINE_HEIGHT = 600;

    public const string ENGINE_TITLE_BAR = "OpenTK3DEngine - development version.";

    // Camera matrices
    public static Matrix4 MODEL      = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-55.0f)); // From tutorial
    public static Matrix4 VIEW       = Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f); // From tutorial
    public static Matrix4 PROJECTION = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), ENGINE_WIDTH / ENGINE_HEIGHT, 0.1f, 100.0f); // From tutorial
  }

}
