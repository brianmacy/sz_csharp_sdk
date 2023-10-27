using System;
using System.Text;
using System.Runtime.InteropServices;
using Senzing;

namespace Senzing
{
public class G2Config
{
    [DllImport ("G2")]
    static extern long G2Config_init(byte[] moduleName, byte[] iniParams, long verboseLogging);
    public static void init(string moduleName, string iniParams, int verboseLogging) {
        HandleError(G2Config_init(Encoding.UTF8.GetBytes(moduleName),Encoding.UTF8.GetBytes(iniParams), verboseLogging));
    }

    [DllImport ("G2")]
    static extern long G2Config_destroy();
    public static void destroy() {
        HandleError(G2Config_destroy());
    }

    struct G2Config_load_result
    {
        public IntPtr response;
        public long returnCode;
    };

    [DllImport ("G2")]
    static extern G2Config_load_result G2Config_load_helper(byte[] config);
    public static IntPtr load(string config) {
        G2Config_load_result result;
        result.response = IntPtr.Zero;
        result.returnCode = 0;
        result = G2Config_load_helper(Encoding.UTF8.GetBytes(config));
        HandleError(result.returnCode);
        return result.response;
    }


    [DllImport ("G2")]
    static extern long G2Config_close_helper(IntPtr handle);
    public static void close(IntPtr handle) {
        HandleError(G2Config_close_helper(handle));
    }


    struct G2Config_save_result
    {
        public IntPtr response;
        public long returnCode;
    };

    [DllImport ("G2")]
    static extern G2Config_save_result G2Config_save_helper(IntPtr handle);
    public static string save(IntPtr handle) {
        G2Config_save_result result;
        result.response = IntPtr.Zero;
        result.returnCode = 0;

        try
        {
            result = G2Config_save_helper(handle);
            HandleError(result.returnCode);
            return Util.UTF8toString(result.response);
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }

    [DllImport ("G2")]
    static extern long G2Config_setDefaultConfigID(long configID);
    public static void setDefaultConfigID(long configID) {
        HandleError(G2Config_setDefaultConfigID(configID));
    }


    struct G2Config_addDataSource_result
    {
        public IntPtr response;
        public long returnCode;
    };

    [DllImport ("G2")]
    static extern G2Config_addDataSource_result G2Config_addDataSource_helper(IntPtr handle, byte[] json);
    public static void addDataSource(IntPtr handle, string dataSourceCode) {
        G2Config_addDataSource_result result;
        result.response = IntPtr.Zero;
        result.returnCode = 0;

        try
        {
            string json = "{\"DSRC_CODE\":\"" + dataSourceCode.Trim().ToUpper() + "\"}";
            result = G2Config_addDataSource_helper(handle, Encoding.UTF8.GetBytes(json));
            HandleError(result.returnCode);
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }


    struct G2Config_listDataSources_result
    {
        public IntPtr response;
        public long returnCode;
    };

    [DllImport ("G2")]
    static extern G2Config_listDataSources_result G2Config_listDataSources_helper(IntPtr handle);
    public static string listDataSources(IntPtr handle) {
        G2Config_listDataSources_result result;
        result.response = IntPtr.Zero;
        result.returnCode = 0;

        try
        {
            result = G2Config_listDataSources_helper(handle);
            HandleError(result.returnCode);
            return Util.UTF8toString(result.response);
        }
        finally
        {
            Util.FreeG2Buffer(result.response);
        }
    }


    [DllImport ("G2")]
    static extern long G2Config_getLastException([MarshalAs(UnmanagedType.LPArray)] byte[] buf, long length);
    [DllImport ("G2")]
    static extern long G2Config_getLastExceptionCode();
    static void HandleError(long retCode)
    {
        if (retCode == 0)
            return;

        long errorCode = G2Config_getLastExceptionCode();
        byte[] buf = new byte[4096];
        if (G2Config_getLastException(buf, buf.Length) != 0)
            G2Exception.HandleError(errorCode, System.Text.Encoding.UTF8.GetString(buf));
        else
            G2Exception.HandleError(errorCode, "Failed to return error message");
    }
}
}

