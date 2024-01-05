using ShortageManager.Models;

namespace ShortageManager.Services;

public interface IShortageService
{
    List<Shortage>? FilterShortages(string user, string? titleFilter = null, DateTime? createdOnStart = null,
        DateTime? createdOnEnd = null, string? categoryFilter = null, string? roomFilter = null);
}
