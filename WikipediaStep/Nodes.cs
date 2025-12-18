namespace WikipediaStep;

public class Nodes
{
    public Page origin { get; set; }
    public List<Page> node = new List<Page>();

    public void Worker(Page origin)
    {
        Page current;
        for (int i = 0; i < origin.urlList.Count; i++)
        {
            current = new Page();
            current.url = origin.urlList[i];
            current.response = current.GetResponse(current.url);
            node.Add(current);
        }
    }
}