namespace WikipediaStep;

public class Page
{
    // Attributes
    public string title { get; set; }

    // Constructors
    public string url => $"https://en.wikipedia.org/wiki/{title}";
    public List<string> urlList = new List<string>();
    public HttpResponseMessage response;
    
    // Methods
    public HttpResponseMessage GetResponse(string url)
    {
        HttpClient client = new HttpClient(); client.DefaultRequestHeaders.Add("User-Agent", "WikiPath/1.0 (Educational Project)");
        HttpResponseMessage response = new HttpResponseMessage();
        
        Thread process = new Thread(() =>
        {
            response = client.GetAsync(url).Result;
        });
        process.Start();
        process.Join();
        
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
                url = "https://en.wikipedia.org/wiki/";
                while (pageString[j] != 34)
                {
                    url += pageString[j];
                    j++;
                }
                Console.WriteLine("Found URL: " + url);
                response.Add(url);
            }
        }
        
        return response;
    }
}