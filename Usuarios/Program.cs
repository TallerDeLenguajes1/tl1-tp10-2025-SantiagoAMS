using System.Text.Json;
using System.Text.Json.Serialization;

internal class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static async Task Main(string[] args)
    {
        var res = await client.GetAsync(@"https://jsonplaceholder.typicode.com/users");
        res.EnsureSuccessStatusCode();
        var response = await res.Content.ReadAsStringAsync();
        var list = JsonSerializer.Deserialize<List<Usuario>>(response);
        foreach (var u in list)
        {
            Console.WriteLine(u.ToString());
        }
        var ser = JsonSerializer.Serialize(list);
        File.WriteAllText(".\\usuarios.json",ser);
    }
    

}