using BotHelper.Model.Clients;
using BotHelper.Model;
using OpenQA.Selenium;

namespace BotHelper.ClientManager;

public class ClientManager
{
    private readonly IWebDriver _web;
    public List<IClient> Clients { get; set; }
    public ClientManager(IWebDriver webDriver){
        _web = webDriver;
        Clients = new();
    }
    public async Task<IEnumerable<ClientResponse>> SendAsync(string Message)
    {
        if (Clients.Count == 0)
        {
            return Enumerable.Empty<ClientResponse>();
        }
        List<Task<ClientResponse>> tasks = new();
        foreach (var item in Clients)
        {
            tasks.Add(item.GetAsync(Message));
        }
        await Task.WhenAll(tasks);
        return tasks.Select(e => e.Result);
    }
}
