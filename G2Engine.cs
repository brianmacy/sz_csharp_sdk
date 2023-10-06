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
static extern int G2_deleteRecord(byte[] dataSourceCode, byte[] recordID);
public static void deleteRecord(string dataSourceCode, string recordID)
{
  HandleError(G2_deleteRecord(Encoding.UTF8.GetBytes(dataSourceCode),Encoding.UTF8.GetBytes(recordID)));
}


[DllImport ("G2")]
static extern int G2_addRecord(byte[] dataSourceCode, byte[] recordID, byte[] jsonData, IntPtr loadID);
public static void addRecord(string dataSourceCode, string recordID, string jsonData)
{
  HandleError(G2_addRecord(Encoding.UTF8.GetBytes(dataSourceCode),Encoding.UTF8.GetBytes(recordID),Encoding.UTF8.GetBytes(jsonData), IntPtr.Zero));
}


[StructLayout(LayoutKind.Sequential)]
struct G2_getEntityByEntityID_V2_result
{
    public IntPtr response;
    public long returnCode;
}

[DllImport ("G2")]
static extern G2_getEntityByEntityID_V2_result G2_getEntityByEntityID_V2_helper(long entityID, long flags);
public static string getEntityByEntityID(long entityID, long flags = 0)
{
  G2_getEntityByEntityID_V2_result result;
  result.response = IntPtr.Zero;
  try
  {
    result = G2_getEntityByEntityID_V2_helper(entityID, flags);
    HandleError(result.returnCode);
    return Util.UTF8toString(result.response);
  }
  finally
  {
    Util.FreeG2Buffer(result.response);
  }
}

[StructLayout(LayoutKind.Sequential)]
struct G2_getEntityByRecordID_V2_result
{
    public IntPtr response;
    public long returnCode;
}

[DllImport ("G2")]
static extern G2_getEntityByRecordID_V2_result G2_getEntityByRecordID_V2_helper(byte[] dataSourceCode, byte[] recordID, long flags);
public static string getEntityByRecordID(string dataSourceCode, string recordID, long flags = 0)
{
  G2_getEntityByRecordID_V2_result result;
  result.response = IntPtr.Zero;
  try
  {
    result = G2_getEntityByRecordID_V2_helper(Encoding.UTF8.GetBytes(dataSourceCode),Encoding.UTF8.GetBytes(recordID), flags);
    HandleError(result.returnCode);
    return Util.UTF8toString(result.response);
  }
  finally
  {
    Util.FreeG2Buffer(result.response);
  }
}

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

static void HandleError(long retCode)
{
  HandleError((int)retCode);
}

  }
}
