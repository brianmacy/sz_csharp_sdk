# sz_csharp_sdk

## Prequisites
* cmake
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
