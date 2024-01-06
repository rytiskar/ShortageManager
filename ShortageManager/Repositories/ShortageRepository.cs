using ShortageManager.Enums;
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

    /* Returns
     * 0 - shortage was added 
     * 1 - shortage was overrided
     * 2 - shortage already exists
    */
    public int SaveShortage(Shortage shortage)
    {
        List<Shortage>? shortages = LoadShortages();
        Shortage? existingShortage = shortages.FirstOrDefault(s => s.Title == shortage.Title && s.Room == shortage.Room);
        
        if(existingShortage == null)
        {
            shortages.Add(shortage);
            SaveShortages(shortages);
            return 0;
        }
        if(existingShortage != null && shortage.Priority > existingShortage.Priority)
        {
            existingShortage.Priority = shortage.Priority;
            existingShortage.CreatedOn = shortage.CreatedOn;
            SaveShortages(shortages);
            return 1; 
        }
        return 2;
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

    public List<Shortage>? LoadUserShortages(string user)
    {
        List<Shortage>? shortages = LoadShortages();

        if (shortages != null)
        {
            if (user == "admin")
            {
                return shortages;
            }
            List<Shortage> userShortages = shortages.Where(s => s.Creator == user).ToList();
            return userShortages;
        }
        return null;
    }


    public bool DeleteShortage(string user, string title, RoomType room)
    {
        bool noShortagesFound = true;
        List<Shortage>? shortages = LoadShortages();
        if(shortages != null)
        {
            if (shortages.RemoveAll(s => s.Creator == user && s.Title == title && s.Room == room) == 0)
                return noShortagesFound;
            else
            {
                SaveShortages(shortages);
                noShortagesFound = false;
            }
        }
        return noShortagesFound;
    }

}
