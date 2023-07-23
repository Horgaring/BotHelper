using System.Text;

public class ClientResponse
{
    public ClientResponse(string name, params string[] content) => (Name,Content) = (name,content);
    public string Name { get; set; }
    public string[] Content { get; set; }
    public override  string ToString()
    {
        StringBuilder sb = new();
        foreach (var item in Content)
        {
            sb.Append(item + "\n");
        }
        return string.Format("{0}\n\n{1}\n\n{2}",Name,Content[0],sb.ToString());
    }
}