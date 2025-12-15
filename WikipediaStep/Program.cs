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

    static int DepthSearch(Page previous, string destination) // Returns -1 for not found: Else depth where destination is found
    {
        // TODO: Int Depth to regulate iterations
        int response = -1;
        bool found = false;

        Page[] node = new Page[previous.urlList.Count];

        for (int i = 0; i < node.Length; i++)
        {
            node[i] = new Page();
            node[i].response = node[i].GetResponse(previous.urlList[i]);
            node[i].urlList = node[i].ExtractURL();

            if (node[i].urlList.Contains(destination))
                return 1;
        }

        if (found == false)
        {
            for (int i = 0; i < node.Length; i++)
            {
                DepthSearch(node[i], destination);
            }
        }
        
        return response;
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