using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

internal class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static Apis? seleccionada = null;

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
        bool c = true;
        while (c)
        {
            Console.Clear();
            Console.WriteLine("==========================");
            Console.WriteLine(" 1 - Consultar API");
            Console.WriteLine(" 2 - Cambiar API");
            Console.WriteLine(" 3 - Mostrar lineas");
            Console.WriteLine(" 4 - Guardar lineas");
            Console.WriteLine(" 4 - Salir");
            Console.WriteLine("==========================");
            Console.WriteLine(" API: " + (seleccionada == null ? "Ninguna" : seleccionada));
            int opc = Utilidades.LeerEntero();

            switch (opc)
            {
                case 1:
                    if (seleccionada == null)
                    {
                        Utilidades.PrintError("Error, no seleccionaste una API...");

                    }
                    switch (seleccionada)
                    {
                        case Apis.Chistes:
                            BuscarChiste();
                            break;
                        case Apis.Universidades:
                            BuscarUniversidad();
                            break;
                    }
                    break;
                case 2:
                    CambiarAPI();
                    break;
                case 3:
                    MostrarLineas();
                    break;
                case 4:
                    GuardarLineas();
                    break;
                case 5:
                    c = false;
                    break;
                default:
                    Utilidades.PrintError("Opción incorrecta...");
                    break;
            }

        }
        return null;
    }
    private static async void BuscarChiste()
    {
        var chiste = await LlamarApi<Chiste>(links[(int)Apis.Universidades]);
        if (chiste.Count() <= 0)
        {
            Utilidades.PrintError("No se obtuvo ningun chiste (no tiene gracia)...");
            return;
        }


    }
    private static async void BuscarUniversidad()
    {
        var uni = await LlamarApi<Universidad>(links[(int)Apis.Chistes] + (Random.Shared.Next(0, 85)));
        if (uni.Count() <= 0)
        {
            Utilidades.PrintError("No se obtuvo ninguna universidad...");
            return;
        }
        //resultados.Add(Encoding.GetEncoding("latin1").GetString());
    }


    private static void CambiarAPI()
    {
        ListarApis();
        var valores = Enum.GetValues<Apis>();
        int n = -1;
        while (n < 0 || n >= valores.Length)
        {
            n = Utilidades.LeerEntero("Ingresa una opción");
            if (n < 0 || n >= valores.Length)
            {
                Utilidades.PrintError("Ingresá una opción válida");
            }
        }
        seleccionada = valores[n];
        Utilidades.PrintSuccess($"Api seleccionada: {seleccionada}");

    }

    private static void ListarApis()
    {
        Console.WriteLine("APIs disponibles");
        int i = 0;
        foreach (var api in Enum.GetValues<Apis>())
        {
            Console.WriteLine($" {i} - {api}");
            i++;
        }

    }

    private static void MostrarLineas()
    {

    }

    private static void GuardarLineas()
    {

    }

    private static async Task<List<T>> LlamarApi<T>(string url) where T : class
    {
        var ret = await client.GetAsync(url);
        ret.EnsureSuccessStatusCode();
        var response = await ret.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<T>>(response);
    }
}