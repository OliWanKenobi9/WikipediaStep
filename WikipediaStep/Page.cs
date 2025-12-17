namespace WikipediaStep;

public class Page
{
    // Constructors
    public string url {get; set;}
    public List<string> urlList = new List<string>();
    public HttpResponseMessage response;
    
    // Methods
    public HttpResponseMessage GetResponse(string url)
    {
        HttpClient client = new HttpClient(); client.DefaultRequestHeaders.Add("User-Agent", "WikiPath/1.0 (Educational Project)");
        HttpResponseMessage response = new HttpResponseMessage();
        
        response = client.GetAsync(url).Result;
        
        return response;
    }

    public List<string> ExtractURL()
    {
        List<string> response = new List<string>();
        string pageString = this.response.Content.ReadAsStringAsync().Result;
        string sub, url;
        int i = 0, j;

        // Search for: href="/wiki/
        for (i = 0; i < pageString.Length-12; i++)
        {
            sub = pageString.Substring(i, 12);
            if (sub == "href=\"/wiki/")
            {
                j = i + 12;
                url = "";
                while (pageString[j] != 34)
                {
                    url += pageString[j];
                    j++;
                }

                if (IsValidArticleLink(url))
                {
                    string fullUrl = "https://en.wikipedia.org/wiki/" + url;
                    Console.WriteLine("Found URL: " + fullUrl);
                    response.Add(fullUrl);
                }
            }
        }
    
        return response;
    }

    private bool IsValidArticleLink(string urlPath)
    {
        if (urlPath.Contains(":"))
        {
            string[] invalidPrefixes = {
                "Wikipedia:", "Help:", "Special:", "Talk:",
                "File:", "Template:", "Category:", "Portal:",
                "MediaWiki:", "User:", "Module:"
            };
        
            foreach (string prefix in invalidPrefixes)
            {
                if (urlPath.StartsWith(prefix))
                    return false;
            }
        }
        if (urlPath.StartsWith("#"))
            return false;
        if (urlPath == "Main_Page")
            return false;
    
        return true;
    }
}