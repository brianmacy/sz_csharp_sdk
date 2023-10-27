CMAKE_MINIMUM_REQUIRED( VERSION 3.12 )

LIST( APPEND CMAKE_MODULE_PATH "${CMAKE_SOURCE_DIR}/cmake-submodules" )

FIND_PACKAGE( CSharp REQUIRED )
INCLUDE( ${CSHARP_USE_FILE} )

IF (NOT WIN32)
  set( CSHARP_SDK "/sdk:4.8")
ENDIF ()

SET ( SOURCE_FILES
  G2Exceptions.cs
  G2Config.cs
  G2ConfigMgr.cs
  G2Product.cs
  G2Engine.cs
  G2EngineFlags.cs
  Util.cs
  )

CSHARP_ADD_LIBRARY ( G2_csharp ${SOURCE_FILES} )

#CSHARP_ADD_EXECUTABLE ( FileLoader G2_csharp.dll Newtonsoft.Json.13.0.3/lib/net45/Newtonsoft.Json.dll FileLoader.cs )
CSHARP_ADD_EXECUTABLE ( FileLoader G2_csharp.dll Newtonsoft.Json.dll FileLoader.cs )

include (CTest)

CSHARP_ADD_EXECUTABLE ( ProductTest G2_csharp.dll ProductTest.cs )
add_test (NAME ProductTest COMMAND ProductTest.exe)

CSHARP_ADD_EXECUTABLE ( EngineTest G2_csharp.dll EngineTest.cs )
add_test (NAME EngineTest COMMAND EngineTest.exe)

CSHARP_ADD_EXECUTABLE ( ConfigTest G2_csharp.dll ConfigTest.cs )
add_test (NAME ConfigTest COMMAND ConfigTest.exe)

ADD_DEPENDENCIES( ProductTest G2_csharp )
ADD_DEPENDENCIES( EngineTest G2_csharp )
ADD_DEPENDENCIES( ConfigTest G2_csharp )



