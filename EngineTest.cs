using System;
using Senzing;

namespace Test
{
class EngineTest
{
    static int Main(string[] args)
    {
        string engineConfig = Environment.GetEnvironmentVariable("SENZING_ENGINE_CONFIGURATION_JSON");
        if (engineConfig == null)
        {
            Console.WriteLine("SENZING_ENGINE_CONFIGURATION_JSON must be defined");
            return -1;
        }
        G2Engine.init("EngineTest",engineConfig,1);

        try
        {
            Console.WriteLine("getEntityByEntityID: " + G2Engine.getEntityByEntityID(-1, -1));
            return -1; // this should fail
        }
        catch (G2Exception e)
        {
            Console.WriteLine("getEntityByEntityID -1: Unknown [SUCCESS]");
        }

        G2Engine.addRecord("TEST", "CS1","{\"NAME_FULL\":\"John Smith\",\"PHONE_NUMBER\":\"5551212\"}");
        Console.WriteLine("addRecord: success");
        Console.WriteLine("getEntityByRecordID: " + G2Engine.getEntityByRecordID("TEST", "CS1", -1));

        G2Engine.deleteRecord("TEST", "CS1");
        Console.WriteLine("deleteRecord: success");

        try
        {
            Console.WriteLine("getEntityByRecordID TEST CS1: " + G2Engine.getEntityByRecordID("TEST", "CS1", -1));
            return -1; // this should fail
        }
        catch (G2Exception e)
        {
            Console.WriteLine("getEntityByRecordID TEST CS1: Unknown [SUCCESS]");
        }


        G2Engine.destroy();
        return 0;
    }
}
}
