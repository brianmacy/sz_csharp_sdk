CMAKE_MINIMUM_REQUIRED( VERSION 3.12 )

LIST( APPEND CMAKE_MODULE_PATH "${CMAKE_SOURCE_DIR}/cmake-submodules" )

FIND_PACKAGE( CSharp REQUIRED )
INCLUDE( ${CSHARP_USE_FILE} )
#set( CSHARP_PLATFORM "x64")

set( CSHARP_SDK "/sdk:7")

SET ( SOURCE_FILES
  G2Exceptions.cs
  G2Product.cs
  G2Engine.cs
  Util.cs
  )

CSHARP_ADD_LIBRARY ( G2_csharp ${SOURCE_FILES} )

include (CTest)

CSHARP_ADD_EXECUTABLE ( ProductTest G2_csharp.dll ProductTest.cs )
add_test (NAME ProductTest COMMAND ProductTest.exe)

CSHARP_ADD_EXECUTABLE ( EngineTest G2_csharp.dll EngineTest.cs )
add_test (NAME EngineTest COMMAND EngineTest.exe)

ADD_DEPENDENCIES( ProductTest G2_csharp )
ADD_DEPENDENCIES( EngineTest G2_csharp )



