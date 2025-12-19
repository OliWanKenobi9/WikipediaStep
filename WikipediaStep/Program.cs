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
                destination.url = "https://en.wikipedia.org/wiki/Sebastos";
                break;
            case false:
                Console.Write("Origin: ");
                origin.url = "https://en.wikipedia.org/wiki/" + Console.ReadLine();
                
                Console.Write("Destination: ");
                destination.url = "https://en.wikipedia.org/wiki/" + Console.ReadLine();
                break;
        }
    }

    static void NodeSearch(string destination, Nodes nodes, int persecond)
    {
        // Check if nodes[i] contains destination.url
        // If i == nodes.Length
        //          i = 0

        bool found = false;
        int j = 0;
        while (found == false)
        {
            for (int i = 0; i < nodes.node.Count; i++)
            {
                if (nodes.node[i].urlList.Contains(destination))
                {
                    Console.WriteLine($"Done searching node {i}: {nodes.node[i].url}");
                    Console.WriteLine($"Destination found in node {i}: {nodes.node[i].url}");
                    Environment.Exit(0);
                }

                Console.WriteLine($"Done searching node {i}: {nodes.node[i].url}");
            }
            Console.WriteLine($"Done searching nodes {j}");
            j++;
            Thread.Sleep(1000/persecond);
        }
    }
    
    static void Main(string[] args)
    {
        /*
         * TODO: Fix    ORIGIN-> NODE -> DESTINATION path to
         *              ORIGIN-> NODE * x -> DESTINATION
         * TODO: If destination hasnt been found in node layer 1, open additional layers
         */
        Input(out Page origin, out Page destination);
        Nodes nodes = new Nodes();
        // Declaration
        
        origin.response = origin.GetResponse(origin.url);
        origin.urlList = origin.ExtractURL();
        
        destination.response = destination.GetResponse(destination.url);
        destination.urlList = destination.ExtractURL();

        if (origin.urlList.Contains(destination.url))
        {
            Console.WriteLine($"Destination found in origin");
        }
        else
        {
            Thread process = new Thread(() => NodeSearch(destination.url, nodes, 60));
            process.Start();
        
            nodes.Worker(origin);
        }
    }
}