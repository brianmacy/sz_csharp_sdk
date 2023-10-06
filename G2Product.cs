using System;
using System.Text;
using System.Runtime.InteropServices;
using Senzing;

namespace Senzing
{
  public class G2Product
  {
[DllImport ("G2")]
static extern int G2Product_init(byte[] moduleName, byte[] iniParams, int verboseLogging);
public static void init(string moduleName, string iniParams, int verboseLogging) { HandleError(G2Product_init(Encoding.UTF8.GetBytes(moduleName),Encoding.UTF8.GetBytes(iniParams), verboseLogging)); }

[DllImport ("G2")]
static extern int G2Product_destroy();
public static void destroy() { HandleError(G2Product_destroy()); }

[DllImport ("G2")]
static extern IntPtr G2Product_version();
public static String version() { return Util.UTF8toString(G2Product_version()); }

[DllImport ("G2")]
static extern IntPtr G2Product_license();
public static String license() { return Util.UTF8toString(G2Product_license()); }


[DllImport ("G2")]
static extern int G2Product_getLastException([MarshalAs(UnmanagedType.LPArray)] byte[] buf, int length);
[DllImport ("G2")]
static extern int G2Product_getLastExceptionCode();
static void HandleError(int retCode)
{
  if (retCode == 0)
    return;

  int errorCode = G2Product_getLastExceptionCode();
  byte[] buf = new byte[4096];
  if (G2Product_getLastException(buf, buf.Length) != 0)
    G2Exception.HandleError(errorCode, System.Text.Encoding.UTF8.GetString(buf));
  else
    G2Exception.HandleError(errorCode, "Failed to return error message");
}
  }
}

