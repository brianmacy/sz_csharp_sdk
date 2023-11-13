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
        G2Engine.purgeRepository("YES, ERASE ALL MY DATA AND ALL PROCESSES HAVE SHUT DOWN!!!");

        try
        {
            Console.WriteLine("getEntityByEntityID: " + G2Engine.getEntityByEntityID(-1, G2EngineFlags.G2_ENTITY_INCLUDE_ALL_RELATIONS | G2EngineFlags.G2_ENTITY_INCLUDE_RECORD_DATA | G2EngineFlags.G2_ENTITY_INCLUDE_RECORD_MATCHING_INFO));
            return -1; // this should fail
        }
        catch (G2NotFoundException e)
        {
            Console.WriteLine("getEntityByEntityID -1: Unknown [SUCCESS]");
        }

        G2Engine.addRecord("TEST", "CS0","{\"NAME_FULL\":\"George Smith\",\"PHONE_NUMBER\":\"5551212\"}");
        Console.WriteLine("addRecord: [SUCCESS]");
        G2Engine.addRecord("TEST", "CS1","{\"NAME_FULL\":\"John Smith\",\"PHONE_NUMBER\":\"5551212\"}");
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
        G2Engine.addRecord("TEST", "CS3","{\"NAME_FULL\":\"John E Smith\",\"PHONE_NUMBER\":\"5551212\"}", sb);
        G2Engine.addRecord("TEST", "CS4","{\"NAME_FULL\":\"John Edward Smith\",\"PHONE_NUMBER\":\"5551212\"}", sb);

        string redoRecord = G2Engine.getRedoRecord();
        while (!string.IsNullOrEmpty(redoRecord))
        {
            Console.WriteLine("redoRecord: " + redoRecord);
            G2Engine.processRedoRecord(redoRecord,sb);
            Console.WriteLine("processRedoRecord withInfo: [SUCCESS]"+sb.ToString());
            redoRecord = G2Engine.getRedoRecord();
        }

        Console.WriteLine("searchByAttributes2: " + G2Engine.searchByAttributes("{\"NAME_FULL\":\"John Smith\"}"));
        // The hardcoded entity IDs are a hack
        // Should use the search results to inquire based on what the RES_ENT_IDs actually are
        // But since this test purges every time, it works
        Console.WriteLine("howEntity: " + G2Engine.howEntityByEntityID(3));
        Console.WriteLine("whyEntities: " + G2Engine.whyEntities(1,3));

        G2Engine.destroy();
        return 0;
    }
}
}
