using System.Text.Json.Serialization;

public class Broma
{

    [JsonPropertyName("type")]
    public string Type { get;}

    [JsonPropertyName("setup")]
    public string Setup { get;}

    [JsonPropertyName("punchline")]
    public string Punchline { get;}

    [JsonPropertyName("id")]
    public int Id { get;}

}