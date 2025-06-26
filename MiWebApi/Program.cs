using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

internal class Program
{
    private static readonly HttpClient client = new HttpClient();

    private static Task Main(string[] args)
    {
        string api = "";
        while (true)
        {
            Console.WriteLine("==========================");
            Console.WriteLine(" 1 - Consultar API");
            Console.WriteLine(" 2 - Cambiar API");
            Console.WriteLine(" 3 - Guardar lineas");
            Console.WriteLine(" 4 - Salir");
            Console.WriteLine("==========================");
            Console.WriteLine(" API: " + api);
            int opc = Utilidades.LeerEntero();

            switch (opc)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    break;
            }
        }


    }

    private static async Task<List<T>> LlamarApi<T>(string url) where T : class
    {
        var ret = await client.GetAsync(url);
        ret.EnsureSuccessStatusCode();
        var response = await ret.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<T>>(response);
    }
}