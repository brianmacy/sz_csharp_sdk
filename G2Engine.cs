using System;
using System.Text;
using System.Runtime.InteropServices;
using Senzing;

namespace Senzing
{
public class G2Engine
{
    [DllImport ("G2")]
    static extern long G2_init(byte[] moduleName, byte[] iniParams, long verboseLogging);
    public static void init(string moduleName, string iniParams, int verboseLogging) {
        HandleError(G2_init(Encoding.UTF8.GetBytes(moduleName),Encoding.UTF8.GetBytes(iniParams), verboseLogging));
    }

    [DllImport ("G2")]
    static extern long G2_destroy();
    public static void destroy() {
        HandleError(G2_destroy());
    }

    [DllImport ("G2")]
    static extern long G2_deleteRecord(byte[] dataSourceCode, byte[] recordID, IntPtr loadID);
    public static void deleteRecord(string dataSourceCode, string recordID)
    {
        HandleError(G2_deleteRecord(Encoding.UTF8.GetBytes(dataSourceCode),Encoding.UTF8.GetBytes(recordID), IntPtr.Zero));
    }


    [DllImport ("G2")]
    static extern long G2_addRecord(byte[] dataSourceCode, byte[] recordID, byte[] jsonData, IntPtr loadID);
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
        result.returnCode = 0;
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


    [StructLayout(LayoutKind.Sequential)]
    struct G2_getRedoRecord_result
    {
        public IntPtr response;
        public long returnCode;
    }

    [DllImport ("G2")]
    static extern G2_getRedoRecord_result G2_getRedoRecord_helper();
    public static string getRedoRecord()
    {
        G2_getRedoRecord_result result;
        result.response = IntPtr.Zero;
        try
        {
            result = G2_getRedoRecord_helper();
            HandleError(result.returnCode);
            return Util.UTF8toString(result.response);
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }


    [DllImport ("G2")]
    static extern long G2_process(byte[] redoRecord);
    public static void processRedoRecord(string redoRecord)
    {
        HandleError(G2_process(Encoding.UTF8.GetBytes(redoRecord)));
    }


    [StructLayout(LayoutKind.Sequential)]
    struct G2_searchByAttributes_V2_result
    {
        public IntPtr response;
        public long returnCode;
    }

    [DllImport ("G2")]
    static extern G2_searchByAttributes_V2_result G2_searchByAttributes_V2_helper(byte[] jsonData, long flags);
    public static string searchByAttributes(string jsonData, long flags = 0)
    {
        G2_searchByAttributes_V2_result result;
        result.response = IntPtr.Zero;
        try
        {
            result = G2_searchByAttributes_V2_helper(Encoding.UTF8.GetBytes(jsonData), flags);
            HandleError(result.returnCode);
            return Util.UTF8toString(result.response);
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }



    [DllImport ("G2")]
    static extern long G2_getLastException([MarshalAs(UnmanagedType.LPArray)] byte[] buf, long length);
    [DllImport ("G2")]
    static extern long G2_getLastExceptionCode();
    [DllImport ("G2")]
    static extern void G2_clearLastException();

    static void HandleError(long retCode)
    {
        Console.WriteLine("retCode: " + retCode);
        if (retCode == 0)
            return;

        long errorCode = G2_getLastExceptionCode();
        byte[] buf = new byte[4096];
        if (G2_getLastException(buf, buf.Length) != 0)
            G2Exception.HandleError(errorCode, System.Text.Encoding.UTF8.GetString(buf));
        else
            G2Exception.HandleError(errorCode, "Failed to return error message");
        G2_clearLastException();
    }

}
}
