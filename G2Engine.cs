using System;
using System.Text;
using System.Runtime.InteropServices;
using Senzing;

namespace Senzing
{
  public class G2Engine
  {
[DllImport ("G2")]
static extern int G2_init(byte[] moduleName, byte[] iniParams, int verboseLogging);
public static void init(string moduleName, string iniParams, int verboseLogging) { HandleError(G2_init(Encoding.UTF8.GetBytes(moduleName),Encoding.UTF8.GetBytes(iniParams), verboseLogging)); }

[DllImport ("G2")]
static extern int G2_destroy();
public static void destroy() { HandleError(G2_destroy()); }


[DllImport ("G2")]
static extern int G2_getLastException([MarshalAs(UnmanagedType.LPArray)] byte[] buf, int length);
[DllImport ("G2")]
static extern int G2_getLastExceptionCode();

static void HandleError(int retCode)
{
  if (retCode == 0)
    return;

  int errorCode = G2_getLastExceptionCode();
  byte[] buf = new byte[4096];
  if (G2_getLastException(buf, buf.Length) != 0)
    G2Exception.HandleError(errorCode, System.Text.Encoding.UTF8.GetString(buf));
  else
    G2Exception.HandleError(errorCode, "Failed to return error message");
}
  }
}

