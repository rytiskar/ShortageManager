using ShortageManager.ClassLibrary.Enums;
using ShortageManager.ClassLibrary.Models;
using System.Diagnostics;
using System.Text.Json;

namespace ShortageManager.ClassLibrary.Repositories;

public class ShortageRepository : IShortageRepository
{
    private readonly string _filepath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Data", "shortages.json");
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

    public void SaveShortages(List<Shortage> shortages)
    {
        string updatedContent = JsonSerializer.Serialize(shortages, _jsonSerializerOptions);
        File.WriteAllText(_filepath, updatedContent);
    }

    public List<Shortage>? LoadShortages()
    {
        string fileContent = File.ReadAllText(_filepath);
        return JsonSerializer.Deserialize<List<Shortage>>(fileContent, _jsonSerializerOptions);
    }
}
