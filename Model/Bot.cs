class Bot
{
    public Telegram.Bot.TelegramBotClient ClinetT { get; set; }
    private readonly string token;
    public Bot(string token){
        this.token = token;
        ClinetT = new(this.token);
    }
    public void Start()
    {
        
        
    }
}