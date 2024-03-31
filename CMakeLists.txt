cmake_minimum_required(VERSION 3.20)

project(demopinvoke)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_BUILD_TYPE Release)

include_directories(${CMAKE_SOURCE_DIR}/pinvoke.library.managed)

file(GLOB_RECURSE LIBRARY_SOURCE_FILES RELATIVE 
    ${CMAKE_SOURCE_DIR} "*.cpp"
)

add_library(pinvoke.library.managed SHARED ${LIBRARY_SOURCE_FILES})