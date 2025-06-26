
using System.Text.Json.Serialization;

public class Universidad{
        [JsonPropertyName("alpha_two_code")]
        public string PaisDosDigitos { get; set; }

        [JsonPropertyName("country")]
        public string Pais { get; set; }

        [JsonPropertyName("state-province")]
        public object Estado_Provincia { get; set; }

        [JsonPropertyName("domains")]
        public List<string> Dominio { get; set; }

        [JsonPropertyName("name")]
        public string Nombre { get; set; }

        [JsonPropertyName("web_pages")]
        public List<string> Paginas { get; set; }
    }