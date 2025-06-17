using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

internal class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static async Task Main(string[] args)
    {
        var l = await ObtenerTareas();
        foreach (var t in l)
        {
            Console.WriteLine($"{t.Id}: \"{t.Title}\" -> {(t.Completed?"Completada":"No completada")}");
        }
    }

    private static async Task<List<Tarea>> ObtenerTareas()
    {
        var ret = await client.GetAsync(@"https://jsonplaceholder.typicode.com/todos/");
        ret.EnsureSuccessStatusCode();
        string responseBody = await ret.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Tarea>>(responseBody);

    }


}