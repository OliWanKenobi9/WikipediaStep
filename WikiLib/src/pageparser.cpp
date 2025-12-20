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

        int i, j;

        for (i = 0; i < length-12; i++) {
            sub = page.substr(i, 12);
            if (sub == "href=\"/wiki/") {
                j = i+12;
                url = "";

                while (page[j] != 34 && j < length) {
                    url += page[j];
                    j++;
                }

                urls.push_back(url);
            }
        }

        result = (char**)malloc((urls.size() + 1) * sizeof(char*));
        for (h = 0; h < urls.size(); h++) {
            result[h] = (char*)malloc(urls[h].length() + 1);
            strcpy(result[h], urls[h].c_str());
        }

        result[urls.size()] = nullptr;
        return result;
    }
}