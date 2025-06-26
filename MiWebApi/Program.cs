using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

internal class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static int indexApiSeleccionada = -1;

    private enum Apis
    {
        Chistes,
        Universidades
    }
    private static readonly string[] links = {
        @"https://official-joke-api.appspot.com/random_joke",
        @"http://universities.hipolabs.com/search?limit=1&country=Argentina&offset="

    };

    private static readonly List<string> resultados = new List<string>();

    private static Task Main(string[] args)
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("==========================");
            Console.WriteLine(" 1 - Consultar API");
            Console.WriteLine(" 2 - Cambiar API");
            Console.WriteLine(" 3 - Guardar lineas");
            Console.WriteLine(" 4 - Salir");
            Console.WriteLine("==========================");
            Console.WriteLine(" API: " + (indexApiSeleccionada < 0 ? "Ninguna" : Enum.GetValues<Apis>()[indexApiSeleccionada]));
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

    private static void BuscarUniversidades()
    {
        string c = Utilidades.LeerString("Ingresa un pais (en ingles)");
        var unis = LlamarApi<Universidad>(links[1]+(Random.Shared.Next(0,85)));
    }

    private static void BuscarBromas()
    {

    }

    private static void CambiarApi()
    {
        Console.WriteLine();
    }

    private static void ListarApis()
    {
        Console.WriteLine("Apis disponibles");
    }

    private static async Task<List<T>> LlamarApi<T>(string url) where T : class
    {
        var ret = await client.GetAsync(url);
        ret.EnsureSuccessStatusCode();
        var response = await ret.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<T>>(response);
    }
}