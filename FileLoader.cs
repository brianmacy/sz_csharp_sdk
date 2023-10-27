using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Senzing;

public class FileLoader {
   public static int Main(string[] args) {
     if (args.Length == 0) {
       Console.WriteLine("Please provide filename(s) to process");
       return -1;
     }

     string engineConfig = Environment.GetEnvironmentVariable("SENZING_ENGINE_CONFIGURATION_JSON");
     if (engineConfig == null)
     {
       Console.WriteLine("SENZING_ENGINE_CONFIGURATION_JSON must be defined");
       return -1;
     }

     G2Engine.init("EngineTest",engineConfig,0); // change 0 -> 1 for debug trace
     G2Engine.purgeRepository("YES, ERASE ALL MY DATA AND ALL PROCESSES HAVE SHUT DOWN!!!");

     SortedSet<long> affectedEntities = new SortedSet<long>();

     foreach (string filename in args) {
       processFile(filename, affectedEntities);
     }

     foreach (long entityID in affectedEntities) {
       try {
         Console.WriteLine(G2Engine.getEntityByEntityID(entityID, G2EngineFlags.G2_ENTITY_INCLUDE_RECORD_DATA | G2EngineFlags.G2_ENTITY_INCLUDE_ALL_RELATIONS | G2EngineFlags.G2_ENTITY_INCLUDE_RELATED_MATCHING_INFO));
       } catch (G2NotFoundException e) {
       }
     }

     G2Engine.destroy();
     return 0;
   }

   private static void processFile(string filename, SortedSet<long> affectedEntities) {
     using (StreamReader read = new StreamReader(filename)) {
         string line;
         while ((line = read.ReadLine()) != null) {
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
        G2Engine.addRecord(rec["DATA_SOURCE"].ToString(), rec["RECORD_ID"].ToString(), line , sb);
        return sb.ToString();
   }

   public static string processRedo(string line) {
        StringBuilder sb = new StringBuilder();
        G2Engine.processRedoRecord(line, sb);
        return sb.ToString();
   }
}

