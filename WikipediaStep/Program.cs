namespace WikipediaStep;

internal class Program
{
    static void Input(out Page origin, out Page destination)
    {
        origin = new Page();
        destination = new Page();
        switch (EnvironmetProperties.Debug)
        {
            case true:
                origin.title = "Theodosius_III";
                destination.title = "Sebastos"; // 2-Step
                break;
            case false:
                Console.Write("Origin: ");
                origin.title = Console.ReadLine();
                
                Console.Write("Destination: ");
                destination.title = Console.ReadLine();
                break;
        }
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
            for (int i = 0; i < origin.urlList.Count; i++)
            {
                Page node = new Page();
                string fullUrl, titlePart;
                
                
                fullUrl = origin.urlList[i];
                titlePart = fullUrl.Replace("https://en.wikipedia.org/wiki/", "");
                node.title = titlePart;

                node.response = node.GetResponse(node.url);
                node.urlList = node.ExtractURL();

                if (node.urlList.Contains(destination.url))
                {
                    Console.WriteLine($"Found in node {i} / {node.url}");
                    break;
                }
            }
            
        }
    }
}