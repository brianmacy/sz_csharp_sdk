using System;
using System.Runtime.InteropServices;

namespace Senzing
{
  class Util
  {
    public static String UTF8toString(IntPtr str)
    {
      return System.Runtime.InteropServices.Marshal.PtrToStringUTF8(str);
    }

    /*
    static unsafe String UTF8toString(byte* str)
    {
      int length = 0;
      for (byte* i = str; *i != 0; i++, length++);
      var convertedArray = new byte[length];
      System.Runtime.InteropServices.Marshal.Copy(new IntPtr(str), convertedArray , 0, length);
      return Encoding.UTF8.GetString(convertedArray, 0, convertedArray.Length);
    }
    */
  }
}