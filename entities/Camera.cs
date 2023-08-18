
// Camera works as an object that modifies global variables for Model View Projection matrices.
// Those variables are global so that any renderable object can access it (we don't have to pass
// those around) but only Camera modifies it.
// We don't have to use any OOP concepts for that since we are only developer working on this
// engine for now.

// How does it work?
// Camera object will received input events (keys or mouse)
// and modify GLOBAL view matrix depending on various of factors.
// It's static since we only want to have one camera.

namespace OpenTK3DEngine
{
  public static class Camera
  {

  }

}
