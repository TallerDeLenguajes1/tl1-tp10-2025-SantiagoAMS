using System.Text.Json.Serialization;

public class Chiste
{

    [JsonPropertyName("type")]
    public string Tipo { get; set; }

    [JsonPropertyName("setup")]
    public string Inicio { get; set; }

    [JsonPropertyName("punchline")]
    public string Remate { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    public override string ToString()
    {
        return $"> {Inicio}\n> {Remate}";
    }

}