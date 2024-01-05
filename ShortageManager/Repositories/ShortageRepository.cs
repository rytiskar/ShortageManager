using ShortageManager.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ShortageManager.Repositories;

public class ShortageRepository : IShortageRepository
{
    private readonly string _filepath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Data", "shortages.json");
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

    public bool SaveShortage(Shortage shortage)
    {
        List<Shortage>? shortages = LoadShortages();
        Shortage? existingShortage = shortages.FirstOrDefault(s => s.Title == shortage.Title && s.Room == shortage.Room);
        bool shortageUpdated = false;
        
        if(existingShortage == null)
        {
            shortages.Add(shortage);
        }
        else if(shortage.Priority > existingShortage.Priority)
        {
            existingShortage.Priority = shortage.Priority;
            existingShortage.CreatedOn = shortage.CreatedOn;
            shortageUpdated = true;
        }

        SaveShortages(shortages);
        return shortageUpdated; 
    }

    private void SaveShortages(List<Shortage> shortages)
    {
        string updatedContent = JsonSerializer.Serialize(shortages, _jsonSerializerOptions);
        File.WriteAllText(_filepath, updatedContent);
    }
    
    private List<Shortage>? LoadShortages()
    {
        string fileContent = File.ReadAllText(_filepath);
        return JsonSerializer.Deserialize<List<Shortage>>(fileContent, _jsonSerializerOptions);
    }

    public void DeleteShortage(Shortage shortage)
    {
        List<Shortage>? shortages = LoadShortages();
        if(shortages != null)
        {
            shortages.RemoveAll(s => s.Title == shortage.Title && s.Name == shortage.Name);
            SaveShortages(shortages);
        }
    }

}
