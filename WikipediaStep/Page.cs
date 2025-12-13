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
    static public HttpResponseMessage GetResponse(string url)
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
}