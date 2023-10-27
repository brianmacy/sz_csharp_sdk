using System;
using System.Text;
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
        G2Engine.init("EngineTest",engineConfig,0); // change 0 -> 1 for debug trace

        try
        {
            Console.WriteLine("getEntityByEntityID: " + G2Engine.getEntityByEntityID(-1, G2EngineFlags.G2_ENTITY_INCLUDE_ALL_RELATIONS | G2EngineFlags.G2_ENTITY_INCLUDE_RECORD_DATA | G2EngineFlags.G2_ENTITY_INCLUDE_RECORD_MATCHING_INFO));
            return -1; // this should fail
        }
        catch (G2NotFoundException e)
        {
            Console.WriteLine("getEntityByEntityID -1: Unknown [SUCCESS]");
        }

        G2Engine.addRecord("TEST", "CS1","{\"NAME_FULL\":\"John Smith\",\"PHONE_NUMBER\":\"5551212\"}");
        Console.WriteLine("addRecord: [SUCCESS]");
        Console.WriteLine("getEntityByRecordID: " + G2Engine.getEntityByRecordID("TEST", "CS1"));
        Console.WriteLine("searchByAttributes: " + G2Engine.searchByAttributes("{\"NAME_FULL\":\"John Smith\"}"));

        G2Engine.deleteRecord("TEST", "CS1");
        Console.WriteLine("deleteRecord: [SUCCESS]");

        try
        {
            Console.WriteLine("getEntityByRecordID UNKNOWNDATASOURCE CS1: " + G2Engine.getEntityByRecordID("UNKNOWNDATASOURCE", "CS1"));
            return -1; // this should fail
        }
        catch (G2UnknownDatasourceException e)
        {
            Console.WriteLine("getEntityByRecordID UNKNOWNDATASOURCE CS1: Unknown [SUCCESS]");
        }

        try
        {
            Console.WriteLine("getEntityByRecordID TEST CS1: " + G2Engine.getEntityByRecordID("TEST", "CS1"));
            return -1; // this should fail
        }
        catch (G2NotFoundException e)
        {
            Console.WriteLine("getEntityByRecordID TEST CS1: Unknown [SUCCESS]");
        }

        StringBuilder sb = new StringBuilder();
        G2Engine.addRecord("TEST", "CS2","{\"NAME_FULL\":\"John Smith\",\"PHONE_NUMBER\":\"5551212\"}", sb);
        Console.WriteLine("addRecord withInfo: [SUCCESS] "+sb.ToString());

        string redoRecord = G2Engine.getRedoRecord();
        while (!string.IsNullOrEmpty(redoRecord))
        {
            Console.WriteLine("redoRecord: " + redoRecord);
            G2Engine.processRedoRecord(redoRecord,sb);
            Console.WriteLine("processRedoRecord withInfo: [SUCCESS]"+sb.ToString());
            redoRecord = G2Engine.getRedoRecord();
        }

        Console.WriteLine("searchByAttributes2: " + G2Engine.searchByAttributes("{\"NAME_FULL\":\"John Smith\"}"));

        G2Engine.destroy();
        return 0;
    }
}
}
