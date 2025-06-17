using System.Text.Json;
using System.Text.Json.Serialization;

public class Tarea
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("completed")]
    public bool Completed { get; set; }

    override public string ToString()
    {
        return $"{(this.Completed ? "Compl." : "Pend.")}\t\"{this.Title}\"";
    }

    public void Print()
    {
        ConsoleColor current = Console.ForegroundColor;
        Console.ForegroundColor = this.Completed ? ConsoleColor.Green : ConsoleColor.Yellow;
        Console.Write($"{(this.Completed ? "Compl." : "Pend.")}\t");
        Console.ForegroundColor = current;
        Console.WriteLine($"\"{this.Title}\"");
    }

}