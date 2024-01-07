using ShortageManager.ClassLibrary.Enums;
using ShortageManager.ClassLibrary.Models;

namespace ShortageManager.ClassLibrary.Repositories;

public interface IShortageRepository
{
    int SaveShortage(Shortage shortage);
    List<Shortage>? LoadUserShortages(string user);
    bool DeleteShortage(string user, string title, RoomType room);
}
