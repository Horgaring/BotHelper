using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace BotHelper.Model.Clients;

public class MailRespClient :   IClient
{
    private readonly string format = "https://otvet.mail.ru/go-proxy/answer_json?q={0}&num=1&sf=0";
    
    
    public  string Name { get; } = "Mail.ruResponse";
    public HttpClient Client { get; set; }
    public string UrlQuestion { get; set; }
    
    
    public MailRespClient(HttpClient client) => Client = client;
    public async Task<ClientResponse> GetAsync(string mess)
    {
        var res = await Client.GetContentAsStringAsync(string.Format(format,mess));
        JObject o = JObject.Parse(res);
        var val = o.SelectToken("results[0].url") as JValue;
        UrlQuestion = val!.Value!.ToString();
        return await Parse();
        
    }
    private async Task<ClientResponse> Parse()
    {
        string head;
        var response = await Client.GetAsync(UrlQuestion);
        var html = new HtmlDocument();
        html.LoadHtml(await response.Content.ReadAsStringAsync());
        var nodes = html.DocumentNode.SelectNodes("//h1[contains(@class, 'z7Rgh')]"); 
        var nodes2 = html.DocumentNode.SelectNodes("//p[contains(@class, 'Xn2FM')]");
        if (nodes.Count < 1 || nodes2.Count < 1)
        {
            throw new HtmlWebException("Not Fount");
        }
        
        if (nodes2.Count > 1)
        {
            head = nodes2[0].InnerText + "\n" + nodes2[1].InnerText;
        }
        else
        {
            head = nodes[0].InnerText;
        }
        return new ClientResponse(Name,head,nodes2[0].InnerText);

    }
    
}