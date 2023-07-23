using OpenQA.Selenium;

namespace BotHelper.Model;
interface IClient
{
    //public  IWebDriver Sel {get; set;}
    public   string Name {get; }
    public Task<ClientResponse> GetAsync(string mess);
}
