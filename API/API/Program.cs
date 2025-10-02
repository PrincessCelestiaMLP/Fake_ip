using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class Person
{
    public string name { get; set; } = string.Empty;
    public string height { get; set; } = string.Empty;
    public string mass { get; set; } = string.Empty;
    public string hair_color { get; set; } = string.Empty;
    public string skin_color { get; set; } = string.Empty;
    public string eye_color { get; set; } = string.Empty;
    public string birth_year { get; set; } = string.Empty;
    public string gender { get; set; } = string.Empty;
    public string homeworld { get; set; } = string.Empty;
    public List<string> films { get; set; } = new();
    public List<string> species { get; set; } = new();
    public List<string> vehicles { get; set; } = new();
    public List<string> starships { get; set; } = new();
    public string created { get; set; } = string.Empty;
    public string edited { get; set; } = string.Empty;
    public string url { get; set; } = string.Empty;
}

class Program
{
    private static readonly HttpClient _http = new HttpClient
    {
        BaseAddress = new Uri("https://swapi.dev/api/"),
        Timeout = TimeSpan.FromSeconds(15)
    };

    static async Task Main()
    {
        try
        {
            Console.WriteLine("Запит до SWAPI...");

            var response = await _http.GetAsync("people/3/"); 
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Raw JSON:");
            Console.WriteLine(json);

            var person = JsonSerializer.Deserialize<Person>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (person != null)
            {
                Console.WriteLine("\n=== Результат парсингу ===");
                Console.WriteLine($"Name: {person.name}");
                Console.WriteLine($"Height: {person.height}");
                Console.WriteLine($"Mass: {person.mass}");
                Console.WriteLine($"Hair color: {person.hair_color}");
                Console.WriteLine($"Eye color: {person.eye_color}");
                Console.WriteLine($"Birth year: {person.birth_year}");
                Console.WriteLine($"Gender: {person.gender}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }
}
