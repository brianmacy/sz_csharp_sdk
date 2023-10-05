CMAKE_MINIMUM_REQUIRED( VERSION 3.12 )

LIST( APPEND CMAKE_MODULE_PATH "${CMAKE_SOURCE_DIR}/cmake-submodules" )

FIND_PACKAGE( CSharp REQUIRED )
INCLUDE( ${CSHARP_USE_FILE} )
#set( CSHARP_PLATFORM "x64")

set( CSHARP_SDK "/sdk:7")

SET ( SOURCE_FILES
  G2Exceptions.cs
  G2Product.cs
  Util.cs
  )

CSHARP_ADD_LIBRARY ( G2_csharp ${SOURCE_FILES} )

include (CTest)

CSHARP_ADD_EXECUTABLE ( ProductTest G2_csharp ProductTest.cs )
add_test (NAME ProductTest COMMAND ProductTest.exe)

ADD_DEPENDENCIES( ProductTest G2_csharp )



