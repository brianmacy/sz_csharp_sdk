using System;
using System.Runtime.InteropServices;
using Senzing;

namespace Senzing
{
  class G2Product
  {
[DllImport ("G2")]
public static extern IntPtr G2Product_version();
public static String version() { return Util.UTF8toString(G2Product_version()); }
[DllImport ("G2")]
public static extern IntPtr G2Product_license();
public static String license() { return Util.UTF8toString(G2Product_license()); }
  }
}
