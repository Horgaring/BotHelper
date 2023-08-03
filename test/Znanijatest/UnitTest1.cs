using BotHelper.Model.Clients;
using OpenQA.Selenium.Firefox;

namespace Znanijatest;

public class UnitTest1
{
    [Fact]
    public async void Test1()
    {
        var wd = new FirefoxDriver();
        var client = new ZnaniaCllent(wd);

        Exception? exception = await Record.ExceptionAsync(async () => await client.GetAsync("помогите решить упражнение"));
        
        Assert.Null(exception);
    }
}