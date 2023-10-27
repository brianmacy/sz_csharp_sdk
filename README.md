# sz_csharp_sdk

## Prerequisites (Linux)
* .NET 4+
* CMake 3.12 or higher
* mono-complete (Ubuntu package, whatever on other distros)
* nuget 2.12 or higher (https://github.com/mono/nuget-binary/tree/2.12 for mono users)
* Senzing API 8.x or higher (currently pre-release)
* Existing Senzing repository (see Senzing Quickstart for Docker)
* Proper LD_LIBRARY_PATH set to include the Senzing libraries
* SENZING_ENGINE_CONFIGURATION_JSON set in environment

## Build/Test
```
export SENZING_ENGINE_CONFIGURATION_JSON=...
export LD_LIBRARY_PATH=...
cmake .
make
make test
```

## Windows build
Nothing fancy here.  I used CMake because I'm familiar with it.  The CMakeLists.txt has a SOURCE_FILES variable that lists the C# files included in the G2 .NET library.  Feel free to build it any way you want.
```
SET ( SOURCE_FILES
  G2Exceptions.cs
  G2Product.cs
  G2Engine.cs
  Util.cs
  )
```
With mono/CMake it builds like this:
```
$ make
[ 33%] Compiling C# library G2_csharp: '/usr/bin/mcs /t:library /out:G2_csharp.dll /platform:anycpu /sdk:4 /reference:System.dll G2Exceptions.cs;G2Product.cs;G2Engine.cs;Util.cs'
[ 33%] Built target G2_csharp
[ 66%] Compiling C# exe ProductTest: '/usr/bin/mcs /t:exe /out:ProductTest.exe /platform:anycpu /sdk:4 /reference:System.dll,G2_csharp.dll ProductTest.cs'
[ 66%] Built target ProductTest
[100%] Compiling C# exe EngineTest: '/usr/bin/mcs /t:exe /out:EngineTest.exe /platform:anycpu /sdk:4 /reference:System.dll,G2_csharp.dll EngineTest.cs'
EngineTest.cs(23,28): warning CS0168: The variable `e' is declared but never used
EngineTest.cs(41,28): warning CS0168: The variable `e' is declared but never used
Compilation succeeded - 2 warning(s)
[100%] Built target EngineTest
$make test
$ make test
Running tests...
Test project /home/bmacy/open_dev/sz_csharp_sdk
    Start 2: EngineTest
    Start 1: ProductTest
1/2 Test #1: ProductTest ......................   Passed    0.02 sec
2/2 Test #2: EngineTest .......................   Passed    2.61 sec

100% tests passed, 0 tests failed out of 2

Total Test time (real) =   2.61 sec
$ doxygen Doxyfile
...
Combining RTF output...
Running plantuml with JAVA...
Running dot...
lookup cache used 27/65536 hits=68 misses=48
finished...
```


## Documentation
Mostly there isn't any.  You can run doxygen Doxyfile and get the class hierarchy and functions.

The simple SomeClassTest.cs files show some basic use.  The main purpose of the functions and parameters can be easily seen at http://docs.senzing.com/

The one main divergence from docs.senzing.com is processRedoRecord.  The .NET interface has processRedoRecord take the record returned from getRedoRecord.  In the current standard docs.senzing.com, processRedoRecord internally does the get and returns the record processed.  EngineTest.cs shows the .NET pattern.

