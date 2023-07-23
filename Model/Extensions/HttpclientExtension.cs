public static class HttpclientExtension
{
    public static async Task<string> GetContentAsStringAsync(this HttpClient client, string url){
        try
        {
            var res = await client.GetAsync(url);
            return await res.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            
            Console.WriteLine(e.InnerException.Message);
            return"";
        }
        
    }
}