#include "pageparser.h"
#include <iostream>

using namespace std;

extern "C" {
    EXPORT void ExtractURL() {
        cout << "ExtractURL" << endl;
    }
}