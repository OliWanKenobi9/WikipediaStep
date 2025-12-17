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
                origin.url = "https://en.wikipedia.org/wiki/Theodosius_III";
                //destination.title = "Sebastos";
                destination.url = "https://en.wikipedia.org/wiki/List_of_Byzantine_emperors";
                break;
            case false:
                Console.Write("Origin: ");
                origin.url = "https://en.wikipedia.org/wiki/" + Console.ReadLine();
                
                Console.Write("Destination: ");
                destination.url = "https://en.wikipedia.org/wiki/" + Console.ReadLine();
                break;
        }
    }

    static bool DepthSearch(Page previous, string destination, int depth = -1) 
        // Parameter depth: Value -1 means no regulation
    {
        bool found = false;
        List<Page> nodes = new List<Page>();
        string sub;
        
        for (int i = 0; i < previous.urlList.Count; i++)
        {
            Page node = new Page();
            node.url = previous.urlList[i];
            node.response = node.GetResponse(node.url);
            node.urlList = node.ExtractURL();
            
            if (node.urlList.Contains(destination))
            {
                found = true;
            }
            nodes.Add(node);
        }

        if (found == false)
        {
            if (depth == -1)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    found = DepthSearch(nodes[i], destination, 1);
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

        Console.WriteLine(DepthSearch(origin, destination.url));
        
        /*if (origin.urlList.Contains(destination.url))
        {
            Console.WriteLine("Destination found in Origin");
        }
        else
        {
            Console.WriteLine(DepthSearch(origin, destination.url));
        }*/
    }
}