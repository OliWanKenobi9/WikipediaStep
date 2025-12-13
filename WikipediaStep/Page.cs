namespace WikipediaStep;

public class Page
{
    // Attributes
    public string title { get; set; }

    // Constructors
    public string url => $"https://en.wikipedia.org/wiki/{title}";
}