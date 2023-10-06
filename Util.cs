using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Senzing
{
  class Util
  {
    public static string UTF8toString(IntPtr str)
    {
      //return System.Runtime.InteropServices.Marshal.PtrToStringUTF8(str);
      int len = 0;
      while (Marshal.ReadByte(str, len) != 0) ++len;
      byte[] buffer = new byte[len];
      Marshal.Copy(str, buffer, 0, buffer.Length);
      return Encoding.UTF8.GetString(buffer);
    }

[DllImport ("G2")]
static extern void G2GoHelper_free(IntPtr p);

    public static void FreeG2Buffer(IntPtr p)
    {
      G2GoHelper_free(p);
    }
  }
}
