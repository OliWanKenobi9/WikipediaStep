using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace WikipediaStep;

internal class Program
{

    [DllImport("libWikiLib.dylib", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr ExtractURL(string pageIn);
    
    static void Input(out Page origin, out Page destination)
    {
        origin = new Page();
        destination = new Page();
        switch (EnvironmentProperties.Debug)
        {
            case true:
                origin.url = "https://en.wikipedia.org/wiki/Saint_Catherine%27s_Monastery";
                destination.url = "https://en.wikipedia.org/wiki/Modular_arithmetic";
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
        bool found = false;
        int lastCheckedIndex = 0;
    
        while (found == false)
        {
            int currentCount = nodes.node.Count;
            
            for (int i = lastCheckedIndex; i < currentCount; i++)
            {
                if (nodes.node[i].urlList.Contains(destination))
                {
                    Console.WriteLine($"Done searching node {i}: {nodes.node[i].url}");
                    Console.WriteLine($"Destination found in node {i}: {nodes.node[i].url}");
                    found = true;
                    Environment.Exit(0);
                }

                Console.WriteLine($"Done searching node {i}: {nodes.node[i].url}");
            }
        
            lastCheckedIndex = currentCount;
        
            Thread.Sleep(1000/persecond);
        }
    }
    
    
    static void Main(string[] args)
    {
        
        Input(out Page origin, out Page destination);
        Nodes nodes = new Nodes(); nodes.origin = origin;
        // Declaration
        
        origin.response = origin.GetResponse(origin.url);
        origin.urlList = origin.ExtractURL();
        
        destination.response = destination.GetResponse(destination.url);
        destination.urlList = destination.ExtractURL();

        if (origin.urlList.Contains(destination.url))
        {
            Console.WriteLine($"Destination found in Origin");
            Environment.Exit(0);
        }
        else
        {
            Thread process = new Thread(() => NodeSearch(destination.url, nodes, 60));
            process.Start();
            
            nodes.Worker(origin);
        }
    }
}