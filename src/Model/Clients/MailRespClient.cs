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
        
        var titleq = html.DocumentNode.SelectSingleNode("//h1[contains(@class, 'q--qtext')]"); 
        var bodyq = html.DocumentNode.SelectSingleNode("//div[contains(@class, 'q--qcomment medium')]");
        var respq    = html.DocumentNode.SelectNodes("//div[contains(@class, 'a--atext atext')]");

        if (titleq is null || respq is null)
        {
            throw new HtmlWebException("Not Fount");
        }
        
        if (bodyq != null)
        {
            head = titleq.InnerText + "\n" + bodyq.InnerText;
        }
        else
        {
            head = titleq.InnerText;
        }
        return new ClientResponse(Name,head,respq[0].InnerText);

    }
    
}