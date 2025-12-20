#include "pageparser.h"
#include <iostream>

extern "C" {
    EXPORT void SayHello() {
        std::cout << "Hi! I'm C++" << std::endl;
    }
    EXPORT void ExtractURL() {

    }
}