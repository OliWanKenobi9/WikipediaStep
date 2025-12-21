#include "pageparser.hpp"
#include <iostream>
#include <string>

using namespace std;

static bool IsValidArticleLink(const string url) {
    int length = url.length();

    // User:, File:, Help:, Talk: (colon at index 4)
    if ((url[0] == 'U' || url[0] == 'F' || url[0] == 'H' || url[0] == 'T') && length > 4) {
        if (url[4] == ':')
            return false;
    }

    if ((url[0] == 'P' || url[0] == 'M') && length > 6) {
        if (url[6] == ':')
            return false;
    }

    if (url[0] == 'S' && length > 7) {
        if (url[7] == ':')
            return false;
    }

    if (url[0] == 'T' && length > 8) {
        if (url[8] == ':')
            return false;
    }

    if (url[0] == 'C' && length > 8) {
        if (url[8] == ':')
            return false;
    }

    if (url[0] == 'M' && length > 9) {
        if (url[9] == ':')
            return false;
    }

    if (url[0] == 'W' && length > 9) {
        if (url[9] == ':')
            return false;
    }

    return true;
}

static vector<string> RemoveDuplicates(vector<string> url) {
    vector<string> uniqueurls;
    bool found;

    for (int i = 0; i < url.size(); i++) {
        found = false;

        for (int h = 0; h < uniqueurls.size(); h++) {
            if (url[i] == uniqueurls[h]) {
                found = true;
                break;
            }
        }

        if (!found)
            uniqueurls.push_back(url[i]);
    }

    return uniqueurls;
}

extern "C" {
    EXPORT char** ExtractURL(const char* pageIn) {
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
                if (IsValidArticleLink(url))
                    urls.push_back(url);
            }
        }

        urls = RemoveDuplicates(urls);

        result = (char**)malloc((urls.size() + 1) * sizeof(char*));
        for (h = 0; h < urls.size(); h++) {
            result[h] = (char*)malloc(urls[h].length() + 1);
            strcpy(result[h], urls[h].c_str());
        }

        result[urls.size()] = nullptr;
        return result;
    }
}