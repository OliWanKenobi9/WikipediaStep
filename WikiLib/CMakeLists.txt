cmake_minimum_required(VERSION 4.0.2)
project(WikiLib)

set(CMAKE_CXX_STANDARD 17)

add_library(WikiLib SHARED
        src/pageparser.cpp
        src/pageparser.cpp
        src/pageparser.cpp
        include/pageparser.h
)

target_include_directories(WikiLib PUBLIC
        ${CMAKE_CURRENT_SOURCE_DIR}/include
)

# Set output directory for easier access from C#
set_target_properties(WikiLib PROPERTIES
        LIBRARY_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/bin"
        RUNTIME_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/bin"
)