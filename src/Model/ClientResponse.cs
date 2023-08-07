using System.Text;

public class ClientResponse
{
    public ClientResponse(string name, params string[] content) => (Name,Content) = (name,content);
    public string Name { get; set; }
    public string[] Content { get; set; }
    public override  string ToString()
    {
        StringBuilder sb = new();
        for  (int i = 1; i < Content.Length; i++)
        {
            sb.Append(Content[i] + "\n");
        }
        return string.Format("{0}\n\n{1}\n\n{2}",Name,Content[0],sb.ToString());
    }
}