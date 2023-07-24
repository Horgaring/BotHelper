using BotHelper.Model.Clients;
using OpenQA.Selenium.Firefox;
var web = new FirefoxDriver();
try
{
var mailru = new ZnaniaCllent(web);
var u = await mailru.GetAsync("помогите решить задание1");
Console.Write(u.ToString());

}
catch (Exception e)
{
            
            Console.WriteLine(e.Message);
            
}
finally
{
    web.Quit();
}
