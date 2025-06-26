using System.Data;
using System.IO;
using System.Reflection;
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
        @"https://official-joke-api.appspot.com/jokes/random/2",
        @"http://universities.hipolabs.com/search?limit=1&country=Argentina&offset="

    };
    private static readonly List<Chiste> chistes = [];
    private static readonly List<Universidad> universidades = [];

#pragma warning disable
    private static async Task Main(string[] args)
    {
#pragma warning enable
        bool c = true;
        while (c)
        {
            Console.Clear();
            Console.WriteLine("==========================");
            Console.WriteLine(" 1 - Consultar API");
            Console.WriteLine(" 2 - Cambiar API");
            Console.WriteLine(" 3 - Mostrar lineas");
            Console.WriteLine(" 4 - Guardar lineas");
            Console.WriteLine(" 5 - Salir");
            Console.WriteLine("==========================");
            /**/
            Console.Write(" API: ");
            if (seleccionada == null)
            {
                Utilidades.WriteColoredLine("Ninguna", ConsoleColor.Red);
            }
            else
            {
                Utilidades.WriteColoredLine(seleccionada.ToString(), ConsoleColor.Green);
            }

            int opc = Utilidades.LeerEntero("\nIngresá una opción:");
            switch (opc)
            {
                case 1:
                    while (seleccionada == null)
                    {
                        Utilidades.PrintError("\nError, no seleccionaste una API...");
                        CambiarAPI();
                    }
                    switch (seleccionada)
                    {
                        case Apis.Chistes:
                            await BuscarChiste();
                            break;
                        case Apis.Universidades:
                            await BuscarUniversidad();
                            break;
                    }

                    break;
                case 2:
                    Console.Clear();
                    CambiarAPI();
                    //Utilidades.Pausa();
                    break;
                case 3:
                    Console.Clear();
                    MostrarRescatado();
                    Utilidades.Pausa();
                    break;
                case 4:
                    Console.Clear();
                    GuardarLineas();
                    Utilidades.Pausa();
                    break;
                case 5:
                    c = false;
                    continue;
                default:
                    Utilidades.PrintError("Opción incorrecta...");
                    Utilidades.Pausa();
                    break;
            }

        }
    }
    private static async Task<bool> BuscarChiste()
    {
        var query = links[(int)Apis.Chistes];
        var c = await LlamarApi<Chiste>(query);
        if (c.Count <= 0)
        {
            Utilidades.PrintError("No se obtuvo ningun chiste (no tiene gracia)...");
            return false;
        }
        ListarChistes(c);
        chistes.AddRange(c);
        Utilidades.Pausa();
        return true;
    }
    private static async Task<bool> BuscarUniversidad()
    {
        var uni = await LlamarApi<Universidad>(links[(int)Apis.Universidades] + (Random.Shared.Next(0, 85)));
        if (uni.Count <= 0)
        {
            Utilidades.PrintError("No se obtuvo ninguna universidad...");
            return false;
        }
        ListarUniversidades(uni);
        universidades.AddRange(uni);
        Utilidades.Pausa();
        return true;

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

    private static void MostrarRescatado()
    {
        Utilidades.WriteColoredLine("Consultas realizadas: ", ConsoleColor.Cyan);
        ListarChistes();
        ListarUniversidades();
    }

    private static void ListarChistes()
    {
        ListarChistes(chistes);
    }
    private static void ListarChistes(List<Chiste> ch)
    {
        Utilidades.WriteColoredLine("-- Chistes: ", ConsoleColor.Cyan);
        foreach (var c in ch)
        {
            Console.WriteLine($"{c}\n");
        }
    }

    private static void ListarUniversidades()
    {
        ListarUniversidades(universidades);
    }
    private static void ListarUniversidades(List<Universidad> unis)
    {
        Utilidades.WriteColoredLine("-- Universidades: ", ConsoleColor.Cyan);
        foreach (var u in unis)
        {
            Console.WriteLine($"{u}\n");
        }
    }

    private static void GuardarLineas()
    {
        if (!Utilidades.LeerBooleano("¿Guardar lineas?"))
        {
            return;
        }
        var valores = Enum.GetValues<Apis>();
        try
        {
            foreach (var v in valores)
            {
                string carpeta = $".\\{v}.json";
                switch (v)
                {
                    case Apis.Chistes:
                        GuardarLista(chistes, carpeta);
                        break;
                    case Apis.Universidades:
                        GuardarLista(universidades, carpeta);
                        break;
                }
            }
            Utilidades.WriteColoredLine("Guardado exitoso", ConsoleColor.Green);
        }
        catch (Exception e)
        {
            Utilidades.PrintError("Ha ocurrido un error: " + e.Message);
        }
    }

    private static async void GuardarLista<T>(List<T> t, string ruta) where T : class
    {
        Console.WriteLine($"Archivo: \"{ruta}\"");

        using (var stream = new FileStream(ruta, FileMode.Create, FileAccess.Write))
        {
            using (var sw = new StreamWriter(stream, Encoding.GetEncoding("latin1")))
            {
                foreach (var a in t)
                {
                    await sw.WriteLineAsync(JsonSerializer.Serialize<T>(a));
                }
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