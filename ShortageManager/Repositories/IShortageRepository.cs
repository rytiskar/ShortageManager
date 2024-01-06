using ShortageManager.Enums;
using ShortageManager.Models;

namespace ShortageManager.Repositories;

public interface IShortageRepository
{
    int SaveShortage(Shortage shortage);
    List<Shortage>? LoadUserShortages(string user);
    bool DeleteShortage(string user, string title, RoomType room);
}
