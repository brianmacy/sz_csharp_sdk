using System;
using Senzing;

namespace Test
{
class ProductTest
{
    static int Main(string[] args)
    {
        string engineConfig = Environment.GetEnvironmentVariable("SENZING_ENGINE_CONFIGURATION_JSON");
        if (engineConfig == null)
        {
            Console.WriteLine("SENZING_ENGINE_CONFIGURATION_JSON must be defined");
            return -1;
        }
        G2Product.init("ProductTest",engineConfig,1);
        Console.WriteLine("version: " + G2Product.version());
        Console.WriteLine("license: " + G2Product.license());
        G2Product.destroy();
        return 0;
    }
}
}
