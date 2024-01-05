using ShortageManager.Models;

namespace ShortageManager.Repositories;

public interface IShortageRepository
{
    void SaveShortage(Shortage shortage);
    void DeleteShortage(string title, string room, string userName);
    List<Shortage> ListShortages(string userName, DateTime? createdOnStart, DateTime? createdOnEnd, string titleFilter, string categoryFilter, string roomFilter);

    // Get all shortages (for administrators)
    List<Shortage> GetAllShortages();
    bool ShortageExists(string title, string room);
    void UpdatePriority(string title, string room, int newPriority);
}
