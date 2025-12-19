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
            node.Add(new Page());
            node[i].url = origin.urlList[i];
            node[i].response = node[i].GetResponse(node[i].url);
            node[i].urlList = node[i].ExtractURL();
        }
    }
}