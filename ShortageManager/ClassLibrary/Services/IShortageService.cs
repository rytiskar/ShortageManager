using ShortageManager.ClassLibrary.Enums;
using ShortageManager.ClassLibrary.Models;

namespace ShortageManager.ClassLibrary.Services;

public interface IShortageService
{
    int RegisterShortage(string user, string title, string name, RoomType room, CategoryType category, int priority);
    List<Shortage>? ListFilteredShortages(string user, string? titleFilter = null, DateTime? createdOnStart = null,
        DateTime? createdOnEnd = null, string? categoryFilter = null, string? roomFilter = null);
    bool DeleteShortage(string user, string title, RoomType room);
}
