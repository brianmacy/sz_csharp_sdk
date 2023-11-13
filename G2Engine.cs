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

    struct G2_deleteRecordWithInfo_result
    {
        public IntPtr response;
        public long returnCode;
    };
    [DllImport ("G2")]
    static extern long G2_deleteRecord(byte[] dataSourceCode, byte[] recordID, IntPtr loadID);
    [DllImport ("G2")]
    static extern G2_deleteRecordWithInfo_result G2_deleteRecordWithInfo_helper(byte[] dataSourceCode, byte[] recordID, IntPtr loadID, long flags);
    public static void deleteRecord(string dataSourceCode, string recordID, StringBuilder withInfo = null)
    {
        if (withInfo == null)
        {
            HandleError(G2_deleteRecord(Encoding.UTF8.GetBytes(dataSourceCode),Encoding.UTF8.GetBytes(recordID), IntPtr.Zero));
        }
        else
        {
            withInfo.Clear();
            G2_deleteRecordWithInfo_result result;
            result.response = IntPtr.Zero;
            result.returnCode = 0;
            try
            {
                result = G2_deleteRecordWithInfo_helper(Encoding.UTF8.GetBytes(dataSourceCode),Encoding.UTF8.GetBytes(recordID), IntPtr.Zero, 0);
                HandleError(result.returnCode);
                withInfo.Append(Util.UTF8toString(result.response));
            }
            finally
            {
                Util.FreeG2Buffer(result.response);
            }
        }

    }

    struct G2_addRecordWithInfo_result
    {
        public IntPtr response;
        public long returnCode;
    };
    [DllImport ("G2")]
    static extern long G2_addRecord(byte[] dataSourceCode, byte[] recordID, byte[] jsonData, IntPtr loadID);
    [DllImport ("G2")]
    static extern G2_addRecordWithInfo_result G2_addRecordWithInfo_helper(byte[] dataSourceCode, byte[] recordID, byte[] jsonData, IntPtr loadID, long flags);
    public static void addRecord(string dataSourceCode, string recordID, string jsonData, StringBuilder withInfo = null)
    {
        if (withInfo == null)
        {
            HandleError(G2_addRecord(Encoding.UTF8.GetBytes(dataSourceCode),Encoding.UTF8.GetBytes(recordID),Encoding.UTF8.GetBytes(jsonData), IntPtr.Zero));
        }
        else
        {
            withInfo.Clear();
            G2_addRecordWithInfo_result result;
            result.response = IntPtr.Zero;
            result.returnCode = 0;
            try
            {
                result = G2_addRecordWithInfo_helper(Encoding.UTF8.GetBytes(dataSourceCode),Encoding.UTF8.GetBytes(recordID),Encoding.UTF8.GetBytes(jsonData), IntPtr.Zero, 0);
                HandleError(result.returnCode);
                withInfo.Append(Util.UTF8toString(result.response));
            }
            finally
            {
                Util.FreeG2Buffer(result.response);
            }
        }
    }


    struct G2_getEntityByEntityID_V2_result
    {
        public IntPtr response;
        public long returnCode;
    }

    [DllImport ("G2")]
    static extern G2_getEntityByEntityID_V2_result G2_getEntityByEntityID_V2_helper(long entityID, long flags);
    public static string getEntityByEntityID(long entityID, G2EngineFlags flags = G2EngineFlags.G2_ENTITY_DEFAULT_FLAGS)
    {
        G2_getEntityByEntityID_V2_result result;
        result.response = IntPtr.Zero;
        result.returnCode = 0;
        try
        {
            result = G2_getEntityByEntityID_V2_helper(entityID, (long)flags);
            HandleError(result.returnCode);
            return Util.UTF8toString(result.response);
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }

    struct G2_getEntityByRecordID_V2_result
    {
        public IntPtr response;
        public long returnCode;
    }

    [DllImport ("G2")]
    static extern G2_getEntityByRecordID_V2_result G2_getEntityByRecordID_V2_helper(byte[] dataSourceCode, byte[] recordID, long flags);
    public static string getEntityByRecordID(string dataSourceCode, string recordID, G2EngineFlags flags = G2EngineFlags.G2_ENTITY_DEFAULT_FLAGS)
    {
        G2_getEntityByRecordID_V2_result result;
        result.response = IntPtr.Zero;
        try
        {
            result = G2_getEntityByRecordID_V2_helper(Encoding.UTF8.GetBytes(dataSourceCode),Encoding.UTF8.GetBytes(recordID), (long)flags);
            HandleError(result.returnCode);
            return Util.UTF8toString(result.response);
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }


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
            string rec = Util.UTF8toString(result.response);
            if (string.IsNullOrEmpty(rec))
                return null;
            return rec;
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }


    struct G2_processWithInfo_result
    {
        public IntPtr response;
        public long returnCode;
    };
    [DllImport ("G2")]
    static extern long G2_process(byte[] redoRecord);
    [DllImport ("G2")]
    static extern G2_processWithInfo_result G2_processWithInfo_helper(byte[] redoRecord, long flags);
    public static void processRedoRecord(string redoRecord, StringBuilder withInfo = null)
    {
        if (withInfo == null)
        {
            HandleError(G2_process(Encoding.UTF8.GetBytes(redoRecord)));
        }
        else
        {
            withInfo.Clear();
            G2_processWithInfo_result result;
            result.response = IntPtr.Zero;
            result.returnCode = 0;
            try
            {
                result = G2_processWithInfo_helper(Encoding.UTF8.GetBytes(redoRecord), 0);
                HandleError(result.returnCode);
                withInfo.Append(Util.UTF8toString(result.response));
            }
            finally
            {
                Util.FreeG2Buffer(result.response);
            }
        }
    }


    struct G2_searchByAttributes_V2_result
    {
        public IntPtr response;
        public long returnCode;
    }

    [DllImport ("G2")]
    static extern G2_searchByAttributes_V2_result G2_searchByAttributes_V2_helper(byte[] jsonData, long flags);
    public static string searchByAttributes(string jsonData, G2EngineFlags flags = G2EngineFlags.G2_SEARCH_BY_ATTRIBUTES_DEFAULT_FLAGS)
    {
        G2_searchByAttributes_V2_result result;
        result.response = IntPtr.Zero;
        try
        {
            result = G2_searchByAttributes_V2_helper(Encoding.UTF8.GetBytes(jsonData), (long)flags);
            HandleError(result.returnCode);
            return Util.UTF8toString(result.response);
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }



    struct G2_howEntityByEntityID_V2_result
    {
        public IntPtr response;
        public long returnCode;
    }

    [DllImport ("G2")]
    static extern G2_howEntityByEntityID_V2_result G2_howEntityByEntityID_V2_helper(long entityID, long flags);
    public static string howEntityByEntityID(long entityID, G2EngineFlags flags = G2EngineFlags.G2_HOW_ENTITY_DEFAULT_FLAGS)
    {
        G2_howEntityByEntityID_V2_result result;
        result.response = IntPtr.Zero;
        result.returnCode = 0;
        try
        {
            result = G2_howEntityByEntityID_V2_helper(entityID, (long)flags);
            HandleError(result.returnCode);
            return Util.UTF8toString(result.response);
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }

    struct G2_whyEntityByEntityID_V2_result
    {
        public IntPtr response;
        public long returnCode;
    }

    [DllImport ("G2")]
    static extern G2_whyEntityByEntityID_V2_result G2_whyEntityByEntityID_V2_helper(long entityID, long flags);
    public static string whyEntityByEntityID(long entityID, G2EngineFlags flags = G2EngineFlags.G2_WHY_ENTITY_DEFAULT_FLAGS)
    {
        G2_whyEntityByEntityID_V2_result result;
        result.response = IntPtr.Zero;
        result.returnCode = 0;
        try
        {
            result = G2_whyEntityByEntityID_V2_helper(entityID, (long)flags);
            HandleError(result.returnCode);
            return Util.UTF8toString(result.response);
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }


    [DllImport ("G2")]
    static extern long G2_purgeRepository();
    static public void purgeRepository(string secretMessage = "DO NOT DO THIS") {
        if (secretMessage != "YES, ERASE ALL MY DATA AND ALL PROCESSES HAVE SHUT DOWN!!!") {
            G2Exception.HandleError(87, "Invalid secretMessage");
        }

        HandleError(G2_purgeRepository());
    }

    [DllImport ("G2")]
    static extern long G2_getLastException([MarshalAs(UnmanagedType.LPArray)] byte[] buf, long length);
    [DllImport ("G2")]
    static extern long G2_getLastExceptionCode();
    [DllImport ("G2")]
    static extern void G2_clearLastException();

    static void HandleError(long retCode)
    {
        if (retCode == 0)
            return;

        if (retCode > 0 || retCode < -2) // there are some cross-platform int size on return issues
            Console.Error.WriteLine("BAD retCode: " + retCode);
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
