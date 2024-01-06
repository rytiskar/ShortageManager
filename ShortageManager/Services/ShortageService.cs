using ShortageManager.Enums;
using ShortageManager.Models;
using ShortageManager.Repositories;
using System.Text.RegularExpressions;

namespace ShortageManager.Services;

public class ShortageService : IShortageService
{
    private readonly IShortageRepository _shortageRepository;

    public ShortageService(IShortageRepository shortageRepository)
    {
        _shortageRepository = shortageRepository;
    }

    public int RegisterShortage(string user, string title, string name, RoomType room, CategoryType category,
        int priority)
    {
        Shortage shortage = new Shortage();
        shortage.Title = title;
        shortage.Name = name;
        shortage.Room = room;
        shortage.Category = category;
        shortage.Priority = priority;
        shortage.CreatedOn = DateTime.Now;
        shortage.Creator = user;

        return _shortageRepository.SaveShortage(shortage);
    }

    public List<Shortage>? ListFilteredShortages(string user, string? titleFilter = null, DateTime? createdOnStart = null,
        DateTime? createdOnEnd = null, string? categoryFilter = null, string? roomFilter = null)
    {
        List<Shortage>? shortages = _shortageRepository.LoadUserShortages(user);

        Regex titleRegex = new Regex(titleFilter ?? "", RegexOptions.IgnoreCase);

        if(shortages != null)
        {
            List<Shortage> filteredShortages = shortages
                .Where(s =>
                    (string.IsNullOrEmpty(titleFilter) || titleRegex.IsMatch(s.Title)) &&
                    (!createdOnStart.HasValue || s.CreatedOn >= createdOnStart.Value) &&
                    (!createdOnEnd.HasValue || s.CreatedOn <= createdOnEnd.Value) &&
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
        return _shortageRepository.DeleteShortage(user, title, room);
    }

}
