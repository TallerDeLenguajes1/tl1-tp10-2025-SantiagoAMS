using System.Text.Json.Serialization;
public class Usuario
{

    [JsonPropertyName("id")]
    public int Id { get; set; }


    [JsonPropertyName("name")]
    public string Nombre { get; set; }


    [JsonPropertyName("username")]
    public string Username { get; set; }


    [JsonPropertyName("email")]
    public string Email { get; set; }


    [JsonPropertyName("address")]
    public Address Domicilio { get; set; }


    [JsonPropertyName("phone")]
    public string Telefono { get; set; }


    [JsonPropertyName("website")]
    public string Website { get; set; }


    [JsonPropertyName("company")]
    public Company Company { get; set; }

    public override string ToString()
    {
        return $"{this.Nombre.PadRight(30)} | {this.Email.PadRight(30)} | {(this.Domicilio.Street+" "+this.Domicilio.Suite).PadRight(40)}";
    }
}
public class Address
{
    [JsonPropertyName("street")]
    public string Street { get; set; }

    [JsonPropertyName("suite")]
    public string Suite { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("zipcode")]
    public string Zipcode { get; set; }

    [JsonPropertyName("geo")]
    public Geo Geo { get; set; }
}

public class Company
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("catchPhrase")]
    public string CatchPhrase { get; set; }

    [JsonPropertyName("bs")]
    public string Bs { get; set; }
}

public class Geo
{
    [JsonPropertyName("lat")]
    public string Lat { get; set; }

    [JsonPropertyName("lng")]
    public string Lng { get; set; }
}



