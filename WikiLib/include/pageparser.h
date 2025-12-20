#ifndef PAGEPARSER_H
#define PAGEPARSER_H

#ifdef _WIN32
    #define EXPORT __declspec(dllexport)
#else
    #define EXPORT __attribute__((visibility("default")))
#endif

extern "C" {
    EXPORT void SayHello();
    EXPORT void ExtractURL();
}

#endif // PAGEPARSER_H