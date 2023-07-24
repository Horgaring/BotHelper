using OpenQA.Selenium;

namespace BotHelper.Model;
public interface IClient
{
    //public  IWebDriver Sel {get; set;}
    public   string Name {get; }
    public Task<ClientResponse> GetAsync(string mess);
}
