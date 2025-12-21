using System.Runtime.InteropServices;

namespace WikipediaStep;

public class Page
{
    // Constructors
    public string url {get; set;}
    public List<string> urlList = new List<string>();
    public HttpResponseMessage response;
    private HttpClient client = new HttpClient();
    
    public List<string> RemoveDuplicates(List<string> origin)
    {
        List<string> response = new List<string>();
        
        for (int i = 0; i < origin.Count; i++)
        {
            if (response.Contains(origin[i]) == false)
                response.Add(origin[i]);
        }

        return response;
    }
    
    // Methods
    public HttpResponseMessage GetResponse(string url)
    {
        client.DefaultRequestHeaders.Add("User-Agent", "WikiPath/1.0 (Educational Project)");
        HttpResponseMessage response = new HttpResponseMessage();
        
        response = client.GetAsync(url).Result;
        
        return response;
    }
    public List<string> ExtractURL()
    {
        [DllImport("libWikiLib.dylib", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr ExtractURL(string pageIn);
        string page = this.response.Content.ReadAsStringAsync().Result, url;
        List<string> response = new List<string>();
        int i = 0;
        IntPtr initResponse, stringPtr = new IntPtr();
        // Declarations
        
        initResponse = ExtractURL(page);

        if (initResponse == IntPtr.Zero)
            return response;

        // Marshal char** into List<string>
        do
        {
            Marshal.FreeHGlobal(stringPtr); // Free Memory
            stringPtr = Marshal.ReadIntPtr(initResponse, i * IntPtr.Size);
            url = $"https://en.wikipedia.org/wiki/{Marshal.PtrToStringAnsi(stringPtr)}";


            if (url != null)
            {
                if (IsValidArticleLink(url))
                    response.Add(url);
            }
            
            i++;
        } while (stringPtr != IntPtr.Zero);

        
        Marshal.FreeHGlobal(initResponse); // Free Memory
        response = RemoveDuplicates(response);
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