using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Senzing;

// Run like: ./FileLoader.exe ~/customers.json ~/watchlist.json ~/reference.json
// These sample data files can be found at: https://github.com/Senzing/truth-sets/tree/main/truthsets/demo
// Since mono uses .NET SDK 4.8, this pulls in Newtonsoft for Json but can be switched to anything

public class FileLoader {
    public static int Main(string[] args) {
        if (args.Length == 0) {
            Console.WriteLine("Please provide filename(s) to process");
            return -1;
        }

        // https://senzing.zendesk.com/hc/en-us/articles/360038774134-G2Engine-Configuration-and-the-Senzing-API
        string engineConfig = Environment.GetEnvironmentVariable("SENZING_ENGINE_CONFIGURATION_JSON");
        if (engineConfig == null)
        {
            Console.WriteLine("SENZING_ENGINE_CONFIGURATION_JSON must be defined");
            return -1;
        }

        // Initialize the engine once
        G2Engine.init("EngineTest",engineConfig,0); // change 0 -> 1 for debug trace

        // Only do this if you really want to purge all your data
        // This MUST be the only processing with the Senzing library loaded when you do this
        // or you may corrupt the newly purged repository as the loaded library caches state.
        G2Engine.purgeRepository("YES, ERASE ALL MY DATA AND ALL PROCESSES HAVE SHUT DOWN!!!");

        SortedSet<long> affectedEntities = new SortedSet<long>();

        foreach (string filename in args) {
            processFile(filename, affectedEntities);
        }

        // Here you take all the deduplicated entity IDs that were affected by the operations.  If
        // you didn't purge this would give you the delta load.
        // https://senzing.zendesk.com/hc/en-us/articles/4417768234131--Advanced-Real-time-replication-and-analytics
        foreach (long entityID in affectedEntities) {
            // This loop can be massively parallelized calling the G2Engine.getEntityByEntityID function from as many threads as
            // the system can effectively handle.
            // Instead of the Console write you probably want to push this information somewhere else
            try {
                // Change the flags based on what you want returned: https://docs.senzing.com/flags/
                Console.WriteLine(G2Engine.getEntityByEntityID(entityID, G2EngineFlags.G2_ENTITY_INCLUDE_RECORD_DATA | G2EngineFlags.G2_ENTITY_INCLUDE_ALL_RELATIONS | G2EngineFlags.G2_ENTITY_INCLUDE_RELATED_MATCHING_INFO));
            } catch (G2NotFoundException e) {
                // If you are doing delta loads, you may want to capture that an entity is now gone to inform the downstream systems.
                // In this case we purged first so we don't care about entities that disappeared during the run.
            }
        }

        G2Engine.destroy();
        return 0;
    }

    private static void processFile(string filename, SortedSet<long> affectedEntities) {
        using (StreamReader read = new StreamReader(filename)) {
            string line;
            while ((line = read.ReadLine()) != null) {
                // This can be massively parallelized
                string info = processLine(line);
                if (info != null) {
                    dynamic rec = JObject.Parse(info);
                    if (rec.AFFECTED_ENTITIES != null) {
                        foreach (dynamic item in rec.AFFECTED_ENTITIES) {
                            affectedEntities.Add((long)item.ENTITY_ID);
                        }
                    }
                }
            }
        }

        string redoRecord;
        while ((redoRecord = G2Engine.getRedoRecord()) != null) {
            // This can be massively parallelized
            // The only quirky thing when you do it that redo processing
            // can generate redo.  So you aren't done until 1) there are
            // no records being processed AND 2) getRedoRecord returns null.
            // Since we are doing this single threaded here, we don't have
            // to worry about that.
            string info = processRedo(redoRecord);
            if (info != null) {
                dynamic rec = JObject.Parse(info);
                if (rec.AFFECTED_ENTITIES != null) {
                    foreach (dynamic item in rec.AFFECTED_ENTITIES) {
                        affectedEntities.Add((long)item.ENTITY_ID);
                    }
                }
            }
        }
    }

    public static string processLine(string line) {
        StringBuilder sb = new StringBuilder();
        dynamic rec = JObject.Parse(line);
        G2Engine.addRecord(rec["DATA_SOURCE"].ToString(), rec["RECORD_ID"].ToString(), line, sb);
        return sb.ToString();
    }

    public static string processRedo(string line) {
        StringBuilder sb = new StringBuilder();
        G2Engine.processRedoRecord(line, sb);
        return sb.ToString();
    }
}

