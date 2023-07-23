using BotHelper.Model;
using Rystem.OpenAi;
using Rystem.OpenAi.Chat;

class ChatGBTClient : IClient
{
    
    public string Name => "ChatGBT";

    public async Task<ClientResponse> GetAsync(string mess)
    {
        OpenAiService.Instance.AddOpenAi(e => e.ApiKey = "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");
        var chat = OpenAiService.Factory.Create();
        var res = await chat.Chat.Request(new ChatMessage{Role = ChatRole.User,Content = mess})
            .WithModel(ChatModelType.Gpt35Turbo)
            .WithTemperature(1)
            .ExecuteAsync();
        return new ClientResponse(Name,res.Choices[0].Message.Content);
    }
}