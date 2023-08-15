using System;

namespace OpenTK3DEngine
{
  public static class Util
  {
    // Printing arrays - floats
    public static void printArray(float[] array)
    {
      Console.WriteLine("[{0}]", string.Join(", ", array));
    }

    // Printing arrays - ints
    public static void printArray(int[] array)
    {
      Console.WriteLine("[{0}]", string.Join(", ", array));
    }
  }

}
