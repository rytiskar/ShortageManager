using ShortageManager.ClassLibrary.Enums;
using ShortageManager.ClassLibrary.Models;
using ShortageManager.ClassLibrary.Repositories;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ShortageManager.ClassLibrary.Services;

public class ShortageService : IShortageService
{
    private readonly IShortageRepository _shortageRepository;

    public ShortageService(IShortageRepository shortageRepository)
    {
        _shortageRepository = shortageRepository;
    }

    /* Returns
     * 0 - shortage was added 
     * 1 - shortage was overrided
     * 2 - shortage already exists
    */
    public int RegisterShortage(string user, string title, string name, RoomType room, CategoryType category,
        int priority)
    {
        Shortage shortage = new Shortage()
        {
            Title = title,
            Name = name,
            Room = room,
            Category = category,
            Priority = priority,
            CreatedOn = DateTime.Now,
            Creator = user,
        };

        List<Shortage>? shortages = _shortageRepository.LoadShortages();
        Shortage? existingShortage = shortages.FirstOrDefault(s => s.Title == shortage.Title && s.Room == shortage.Room);

        if (existingShortage == null)
        {
            shortages.Add(shortage);
            _shortageRepository.SaveShortages(shortages);
            return 0;
        }
        if (existingShortage != null && shortage.Priority > existingShortage.Priority)
        {
            existingShortage.Priority = shortage.Priority;
            existingShortage.CreatedOn = shortage.CreatedOn;
            _shortageRepository.SaveShortages(shortages);
            return 1;
        }
        return 2;
    }

    private List<Shortage>? GetUserShortages(string user)
    {
        List<Shortage>? shortages = _shortageRepository.LoadShortages();

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

    public List<Shortage>? ListFilteredShortages(string user, string? titleFilter = null, DateTime? createdOnStart = null,
        DateTime? createdOnEnd = null, string? categoryFilter = null, string? roomFilter = null)
    {
        List<Shortage>? shortages = GetUserShortages(user);

        Regex titleRegex = new Regex(titleFilter ?? "", RegexOptions.IgnoreCase);

        if(shortages != null)
        {
            List<Shortage> filteredShortages = shortages
                .Where(s =>
                    (string.IsNullOrEmpty(titleFilter) || titleRegex.IsMatch(s.Title)) &&
                    (!createdOnStart.HasValue || s.CreatedOn.Date >= createdOnStart.Value.Date) &&
                    (!createdOnEnd.HasValue || s.CreatedOn.Date <= createdOnEnd.Value.Date) &&
                    (string.IsNullOrEmpty(categoryFilter) || s.Category.ToString().Equals(categoryFilter, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(roomFilter) || s.Room.ToString().Equals(roomFilter, StringComparison.OrdinalIgnoreCase)))
                .OrderByDescending(s => s.Priority) 
                .ToList();
            return filteredShortages;
        }
        return null;
    }

    public bool DeleteShortage(string user, string title, RoomType room)
    {
        bool noShortagesFound = true;
        List<Shortage>? shortages = _shortageRepository.LoadShortages();
        if (shortages != null)
        {
            if(user == "admin")
            {
                if (shortages.RemoveAll(s => s.Title == title && s.Room == room) == 0)
                    return noShortagesFound;
                else
                {
                    _shortageRepository.SaveShortages(shortages);
                    noShortagesFound = false;
                }
            }
            else
            {
                if (shortages.RemoveAll(s => s.Creator == user && s.Title == title && s.Room == room) == 0)
                    return noShortagesFound;
                else
                {
                    _shortageRepository.SaveShortages(shortages);
                    noShortagesFound = false;
                }
            }
        }
        return noShortagesFound;
    }

}
