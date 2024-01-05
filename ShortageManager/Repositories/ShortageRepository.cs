using ShortageManager.Models;
using System.Text.Json;

namespace ShortageManager.Repositories;

public class ShortageRepository
{
    private readonly string _filepath = @"Data\shortages.json";
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

    void SaveShortage(Shortage shortage)
    {
        var shortageJson = JsonSerializer.Serialize(shortage, _jsonSerializerOptions);

        File.WriteAllText(_filepath, shortageJson);
    }

}
