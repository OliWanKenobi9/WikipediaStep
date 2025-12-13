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
                origin.title = "Lando_Norris";
                destination.title = "McLaren";
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
    }
}