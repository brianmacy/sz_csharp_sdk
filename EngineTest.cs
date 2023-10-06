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
      G2Engine.destroy();
      return 0;
    }
  }
}
