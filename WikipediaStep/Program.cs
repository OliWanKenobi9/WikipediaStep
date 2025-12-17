namespace WikipediaStep;

internal class Program
{
    static void Input(out Page origin, out Page destination)
    {
        origin = new Page();
        destination = new Page();
        switch (EnvironmentProperties.Debug)
        {
            case true:
                origin.title = "Theodosius_III";
                destination.title = "Sebastos";
                break;
            case false:
                Console.Write("Origin: ");
                origin.title = Console.ReadLine();
                
                Console.Write("Destination: ");
                destination.title = Console.ReadLine();
                break;
        }
    }

    static bool DepthSearch(Page previous, string destination, int depth = -1) 
        // Parameter depth: Value -1 means no regulation
    {
        bool found = false;
        Page[] node = new Page[previous.urlList.Count];
        string sub;
        
        for (int i = 0; i < previous.urlList.Count; i++)
        {
            sub = previous.urlList[i].Replace("https://en.wikipedia.org/wiki/", "");
            node[i] = new Page();
            node[i].title = sub;
            node[i].response = node[i].GetResponse(node[i].url);
            node[i].urlList = node[i].ExtractURL();

            if (node[i].urlList.Contains(destination))
                found = true;
        }

        if (found == false)
        {
            if (depth == -1)
            {
                for (int i = 0; i < node.Length; i++)
                {
                    DepthSearch(node[i], destination, 1);
                }
            }
        }
        else
        {
            Console.WriteLine("Found destination");
            Environment.Exit(0);
        }
        return found;
    }
    
    static void Main(string[] args)
    {
        Input(out Page origin, out Page destination);
        origin.response = origin.GetResponse(origin.url);
        origin.urlList = origin.ExtractURL();

        destination.response = destination.GetResponse(destination.url);
        destination.urlList = destination.ExtractURL();

        if (origin.urlList.Contains(destination.url))
        {
            Console.WriteLine("Destination found in Origin");
        }
        else
        {
            Console.WriteLine(DepthSearch(origin, destination.url));
        }
    }
}