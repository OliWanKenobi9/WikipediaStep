namespace WikipediaStep;

public class Nodes
{
    public Page origin { get; set; }
    public Page[] node => new Page[origin.urlList.Count];

    void Worker(Page origin)
    {
        for (int i = 0; i < origin.urlList.Count; i++)
        {
            node[i].url = origin.urlList[i];
            node[i].response = node[i].GetResponse(node[i].url);
        }
    }
}