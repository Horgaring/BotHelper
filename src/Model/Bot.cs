public class Bot : IDisposable
{
    public Telegram.Bot.TelegramBotClient ClinetT { get; set; }
    public IWebDriver Wd { get; set; }
    public ClientManager Manager { get; set; }
    private readonly string token;
    private bool disposedValue;

    public Bot(string token, IWebDriver wd){
        this.token = token;
        ClinetT = new(this.token);
        Wd = wd;
        Manager = new(Wd);
        Manager.Clients.AddRange(new List<IClient>{new ZnaniaCllent(Manager.web),new MailRespClient(new())});
    }
    public void Start()
    {
       
        ClinetT.StartReceiving(Update,Error);
    }
    private static async  Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    {
        Console.WriteLine(arg2.Message);
        await Task.Delay(1);
    }

    private async Task Update(ITelegramBotClient client, Update update, CancellationToken cancelltoken)
    {
        if(update.Message is Message mess){
            var time = mess.Date +new TimeSpan(0,4,0);
            if (mess.Text == null ||  mess.Date +new TimeSpan(0,4,0)  <= DateTime.Now )
            {
                return;
            }
            if (mess.Text.ToLower() == "Я голубь")
            {
                for (var i = 1000; i > 0;)
                {
                    await Task.Delay(400, cancelltoken);
                    await client.SendTextMessageAsync(mess.Chat.Id, $"{i} - 7 = {i -= 7}",
                        cancellationToken: cancelltoken);
                }
            }
            await client.SendTextMessageAsync(mess.Chat.Id, $"Ожидаю ответа",
                cancellationToken: cancelltoken);
           var res = await Manager.SendAsync(mess.Text);
           foreach (var item in res)
           {
               await client.SendTextMessageAsync(mess.Chat.Id,item.ToString(),
                   cancellationToken: cancelltoken);
           }
        }
            
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Wd.Quit();
            }

            
            disposedValue = true;
        }
    }

    

    void IDisposable.Dispose()
    {
        
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}