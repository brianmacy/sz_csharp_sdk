using System;
using Senzing;

namespace Test
{
class ConfigTest
{
    static int Main(string[] args)
    {
        string engineConfig = Environment.GetEnvironmentVariable("SENZING_ENGINE_CONFIGURATION_JSON");
        if (engineConfig == null)
        {
            Console.WriteLine("SENZING_ENGINE_CONFIGURATION_JSON must be defined");
            return -1;
        }
        G2ConfigMgr.init("ConfigTest",engineConfig,1);
        Console.WriteLine("Default Config ID: " + G2ConfigMgr.getDefaultConfigID());
        Console.WriteLine("Default Config: " + G2ConfigMgr.getConfig(G2ConfigMgr.getDefaultConfigID()));
        Console.WriteLine("Config List: " + G2ConfigMgr.getConfigList());

        G2Config.init("ConfigTest",engineConfig,1);
        long configID = G2ConfigMgr.getDefaultConfigID();
        string config = G2ConfigMgr.getConfig(configID);
        IntPtr handle = G2Config.load(config);
        Console.WriteLine("G2Config.load success");
        Console.WriteLine("listDataSources: " + G2Config.listDataSources(handle));
        G2Config.addDataSource(handle, "MY_DATASOurce ");
        Console.WriteLine("addDataSource: success");
        Console.WriteLine("listDataSources: " + G2Config.listDataSources(handle));
        string newConfig = G2Config.save(handle);
        Console.WriteLine("New Config: " + newConfig);
        G2Config.close(handle);
        Console.WriteLine("G2Config.close: success");

        long newConfigID = G2ConfigMgr.addConfig(newConfig);
        Console.WriteLine("addConfig: " + newConfigID);
        G2ConfigMgr.setDefaultConfigID(newConfigID);
        Console.WriteLine("setDefaultConfigID: success");
        Console.WriteLine("Default Config ID: " + G2ConfigMgr.getDefaultConfigID());
        Console.WriteLine("Config List: " + G2ConfigMgr.getConfigList());

        G2Config.destroy();
        G2ConfigMgr.destroy();
        return 0;
    }
}
}
