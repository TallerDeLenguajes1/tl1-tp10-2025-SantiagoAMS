internal class Program
{
    private static readonly HttpClient client = new HttpClient();

    private static Task Main(string[] args)
    {
        


    }

    private static Task<List<Object>> Obtener(string url, Class c){
        var ret = await client.GetAsync(@"https://");
        ret.EnsureSuccessStatusCode();
        var response = await ret.Content.ReadAsStringAsync();
    }

}