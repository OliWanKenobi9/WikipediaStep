#include "pageparser.h"
#include <iostream>
#include <string>
#include <cstring>

using namespace std;

extern "C" {
    EXPORT char** ExtractURL(const char* pageIn) {
        /*
         * TODO: Add Validation Check
         * TODO: Add RemoveDuplicates
         * TODO: Remove FullURL:
         */
        vector<string> urls;
        string page(pageIn), sub, url;
        size_t length = page.length(), h;
        char** result;

        int i, j, count;;

        for (i = 0; i < length-12; i++) {
            sub = page.substr(i, 12);

            if (sub == "href=\"/wiki/") {
                j = i+12;
                url = "";

                while (page[j] != 34) { // 34 == "
                    url += page[j];
                    j++;
                }

                urls[count] = url;

                count++;
            }

            result = new char*[urls.size() + 1];

            for (h = 0; h < urls.size(); h++) {
                result[h] = new char[urls[h].length() + 1];  // +1 for null terminator
                strcpy(result[h], urls[h].c_str());
            }

            result[urls.size()] = nullptr;

            return result;
        }
    }
}