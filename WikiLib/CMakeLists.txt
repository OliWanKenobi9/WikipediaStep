cmake_minimum_required(VERSION 3.15)
project(WikiLib)

set(CMAKE_CXX_STANDARD 17)

add_library(WikiLib SHARED
        src/mylib.cpp
        include/pageparser.cpp
)

target_include_directories(wikilib PUBLIC
        ${CMAKE_CURRENT_SOURCE_DIR}/include
)

# Set output directory for easier access from C#
set_target_properties(wikilib PROPERTIES
        LIBRARY_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/bin"
        RUNTIME_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/bin"
)