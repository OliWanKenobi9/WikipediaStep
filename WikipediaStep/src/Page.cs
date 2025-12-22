using System.Runtime.InteropServices;

namespace WikipediaStep;

public class Page
{
    // Constructors
    public string url {get; set;}
    public List<string> urlList = new List<string>();
    public HttpResponseMessage response;
    private HttpClient client = new HttpClient();
    [DllImport("libWikiLib.dylib", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr ProcessPageUrl(string pageIn);
    
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
        string page = this.response.Content.ReadAsStringAsync().Result, url;
        List<string> response = new List<string>();
        int i = 0;
        IntPtr initResponse, stringPtr = new IntPtr();
        // Declarations
        
        initResponse = ProcessPageUrl(page);

        if (initResponse == IntPtr.Zero)
            return response;

        // Marshal char** into List<string>
        do
        {
            stringPtr = Marshal.ReadIntPtr(initResponse, i * IntPtr.Size);
            url = $"https://en.wikipedia.org/wiki/{Marshal.PtrToStringAnsi(stringPtr)}";
            Marshal.FreeHGlobal(stringPtr); // Free Memory
            
            if (url != null) 
            {
                response.Add(url);
            }
            
            i++;
        } while (stringPtr != IntPtr.Zero);

        
        Marshal.FreeHGlobal(initResponse); // Free Memory
        return response;
    }
}