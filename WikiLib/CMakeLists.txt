cmake_minimum_required(VERSION 3.15)
project(WikiLib)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_VISIBILITY_PRESET hidden)
set(CMAKE_VISIBILITY_INLINES_HIDDEN YES)

add_library(WikiLib SHARED
        src/pageparser.cpp
)

target_include_directories(WikiLib PUBLIC
        ${CMAKE_CURRENT_SOURCE_DIR}/include
)


set_target_properties(WikiLib PROPERTIES
        LIBRARY_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/bin"
        RUNTIME_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/bin"
        CXX_VISIBILITY_PRESET default
)