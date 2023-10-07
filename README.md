# sz_csharp_sdk

## Prerequisites
* CMake 3.12 or higher
* .NET SDK 7.x or higher
* Senzing API 8.x or higher (currently pre-release)
* Existing Senzing repository (see Senzing Quickstart for Docker)
* Proper PATH/LD_LIBRARY_PATH/etc set to include the Senzing libraries
* SENZING_ENGINE_CONFIGURATION_JSON set in environment

## Build
```
cmake .
make
make test
```

## Documentation
Mostly there isn't any.  You can run doxygen Doxyfile and get the class hierarchy and functions.

The simple <name>Test.cs files show some basic use.  The main purpose of the functions and parameters can be easily seen at http://docs.senzing.com/

The one main divergence from docs.senzing.com is processRedoRecord.  The .NET interface has processRedoRecord take the record returned from getRedoRecord.  In the current standard docs.senzing.com, processRedoRecord internally does the get and returns the record processed.  EngineTest.cs shows the .NET pattern.

