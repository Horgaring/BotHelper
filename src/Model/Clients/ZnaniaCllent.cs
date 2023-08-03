using System.Collections.ObjectModel;
using System.Text;
using BotHelper.Model;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;

namespace BotHelper.Model.Clients;
public class ZnaniaCllent : IClient
{
    private readonly string format = "https://znanija.com/app/ask?q={0}";
    
    
    public  string Name { get; } = "Znanija.com";
    public IWebDriver  Wd { get; set; }
    public int defultque = 1;
    
    public ZnaniaCllent(IWebDriver wd) => Wd = wd;
    
    
    public  Task<ClientResponse> GetAsync(string mess)
    {
        Wd.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);
        Wd.Navigate().GoToUrl(string.Format(format,mess));
        if (Wd.FindElements(By.Id("didomi-notice-agree-button")) is ReadOnlyCollection<IWebElement> els)
        {
            els.FirstOrDefault()?.Click();
        }
        if (Wd.FindElement(By.ClassName("sg-dialog__close-button")) is IWebElement el)
        {
            el.Click();
        }
        Wd.FindElements(By.ClassName("sg-text--link"))[6 + defultque].Click();
        return  Task.FromResult<ClientResponse>(Parse());
        
    }
    public  ClientResponse Parse()
    {
        var question = Wd.FindElement(By.ClassName("QuestionBoxContent-module__primary--F++oO"));
        var res = Wd.FindElements(By.ClassName("AnswerBoxContent-module__richContent--XAbww"));
        var st =string.Join(" ", res.Select(p=> p.Text));
        return new ClientResponse(Name,question.Text,st);
    }
    
}