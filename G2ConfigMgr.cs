using System;
using System.Text;
using System.Runtime.InteropServices;
using Senzing;

namespace Senzing
{
public class G2ConfigMgr
{
    [DllImport ("G2")]
    static extern long G2ConfigMgr_init(byte[] moduleName, byte[] iniParams, long verboseLogging);
    public static void init(string moduleName, string iniParams, int verboseLogging) {
        HandleError(G2ConfigMgr_init(Encoding.UTF8.GetBytes(moduleName),Encoding.UTF8.GetBytes(iniParams), verboseLogging));
    }

    [DllImport ("G2")]
    static extern long G2ConfigMgr_destroy();
    public static void destroy() {
        HandleError(G2ConfigMgr_destroy());
    }

    struct G2ConfigMgr_addConfig_result
    {
        public long configID;
        public long returnCode;
    };

    [DllImport ("G2")]
    static extern G2ConfigMgr_addConfig_result G2ConfigMgr_addConfig_helper(byte[] config, byte[] comments);
    public static long addConfig(string config, string comments = "") {
        G2ConfigMgr_addConfig_result result;
        result.configID = 0;
        result.returnCode = 0;
        result = G2ConfigMgr_addConfig_helper(Encoding.UTF8.GetBytes(config),Encoding.UTF8.GetBytes(comments));
        HandleError(result.returnCode);
        return result.configID;
    }

    [DllImport ("G2")]
    static extern long G2ConfigMgr_setDefaultConfigID(long configID);
    public static void setDefaultConfigID(long configID) {
        HandleError(G2ConfigMgr_setDefaultConfigID(configID));
    }


    struct G2ConfigMgr_getDefaultConfigID_result
    {
        public long configID;
        public long returnCode;
    };

    [DllImport ("G2")]
    static extern G2ConfigMgr_getDefaultConfigID_result G2ConfigMgr_getDefaultConfigID_helper();
    public static long getDefaultConfigID() {
        G2ConfigMgr_getDefaultConfigID_result result;
        result.configID = 0;
        result.returnCode = 0;
        result = G2ConfigMgr_getDefaultConfigID_helper();
        HandleError(result.returnCode);
        return result.configID;
    }


    struct G2ConfigMgr_getConfig_result
    {
        public IntPtr response;
        public long returnCode;
    };

    [DllImport ("G2")]
    static extern G2ConfigMgr_getConfig_result G2ConfigMgr_getConfig_helper(long configID);
    public static string getConfig(long configID) {
        G2ConfigMgr_getConfig_result result;
        result.response = IntPtr.Zero;
        result.returnCode = 0;

        try
        {
            result = G2ConfigMgr_getConfig_helper(configID);
            HandleError(result.returnCode);
            return Util.UTF8toString(result.response);
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }

    struct G2ConfigMgr_getConfigList_result
    {
        public IntPtr response;
        public long returnCode;
    };

    [DllImport ("G2")]
    static extern G2ConfigMgr_getConfigList_result G2ConfigMgr_getConfigList_helper();
    public static string getConfigList() {
        G2ConfigMgr_getConfigList_result result;
        result.response = IntPtr.Zero;
        result.returnCode = 0;

        try
        {
            result = G2ConfigMgr_getConfigList_helper();
            HandleError(result.returnCode);
            return Util.UTF8toString(result.response);
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }


    [DllImport ("G2")]
    static extern long G2ConfigMgr_getLastException([MarshalAs(UnmanagedType.LPArray)] byte[] buf, long length);
    [DllImport ("G2")]
    static extern long G2ConfigMgr_getLastExceptionCode();
    static void HandleError(long retCode)
    {
        if (retCode == 0)
            return;

        long errorCode = G2ConfigMgr_getLastExceptionCode();
        byte[] buf = new byte[4096];
        if (G2ConfigMgr_getLastException(buf, buf.Length) != 0)
            G2Exception.HandleError(errorCode, System.Text.Encoding.UTF8.GetString(buf));
        else
            G2Exception.HandleError(errorCode, "Failed to return error message");
    }
}
}

