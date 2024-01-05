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

    public List<Shortage>? FilterShortages(string user, string? titleFilter = null, DateTime? createdOnStart = null,
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

}
