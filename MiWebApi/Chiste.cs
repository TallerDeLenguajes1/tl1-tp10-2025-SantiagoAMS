using System.Text.Json.Serialization;

public class Chiste
{

    [JsonPropertyName("type")]
    public string Tipo { get; }

    [JsonPropertyName("setup")]
    public string Inicio { get; }

    [JsonPropertyName("punchline")]
    public string Remate { get; }

    [JsonPropertyName("id")]
    public int Id { get; }

    public override string ToString()
    {
        return $"> {Inicio}\n> {Remate}";
    }

}