using ShortageManager.Models;

namespace ShortageManager.Repositories;

public interface IShortageRepository
{
    bool SaveShortage(Shortage shortage);
    /*void SaveShortages(List<Shortage> shortages);
    List<Shortage>? LoadShortages();*/
    void DeleteShortage(Shortage shortage);
/*    List<Shortage> ListShortages(string userName, DateTime? createdOnStart, DateTime? createdOnEnd, string titleFilter, string categoryFilter, string roomFilter);

    // Get all shortages (for administrators)
    List<Shortage> GetAllShortages();
    bool ShortageExists(string title, string room);
    void UpdatePriority(string title, string room, int newPriority);*/
}
