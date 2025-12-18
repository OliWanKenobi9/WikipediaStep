namespace WikipediaStep;

public class Nodes
{
    public Page origin { get; set; }
    public List<Page> node;

    public void Worker(Page origin)
    {
        for (int i = 0; i < origin.urlList.Count; i++)
        {
            node.Add(new Page());
            node[i].url = origin.urlList[i];
            node[i].response = node[i].GetResponse(node[i].url);
        }
    }
}