
// Camera works as an object that modifies global variables for Model View Projection matrices.
// Those variables are global so that any renderable object can access it (we don't have to pass
// those around) but only Camera modifies it.
// We don't have to use any OOP concepts for that since we are only developer working on this
// engine for now.

// How does it work?
// Camera object will received input events (keys or mouse)
// and modify GLOBAL view matrix depending on various of factors.
// It's static since we only want to have one camera.
using OpenTK.Mathematics;

namespace OpenTK3DEngine
{
  public static class Camera
  {
    // Basically modifies View matrix ??
    // Members
    /*
    static Vector3 cameraPos = new Vector3(0.0f, 0.0f, 3.0f);
    static Vector3 cameraTarget = Vector3.Zero;
    static Vector3 cameraDirection = Vector3.Normalize(cameraPos - cameraTarget);
    static Vector3 up = Vector3.UnitY;
    static Vector3 cameraRight = Vector3.Normalize(Vector3.Cross(up, cameraDirection));
    static Vector3 cameraUp = Vector3.Cross(cameraDirection, cameraRight);
    */

    public static float speed = 15.0f;
    public static float sensitivity = 0.0015f;

    public static Vector3 position = new Vector3(0.0f, 0.0f, 3.0f);
    public static Vector3 front    = new Vector3(0.0f, 0.0f, -1.0f);
    public static Vector3 up       = new Vector3(0.0f, 1.0f, 0.0f);

    public static Matrix4 view = Matrix4.LookAt(new Vector3(0.0f, 0.0f, 3.0f),
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 0.0f));

    public static float yaw = 0.0f;
    public static float pitch = 0.0f;

  }

}
