using ShortageManager.ClassLibrary.Enums;
using ShortageManager.ClassLibrary.Models;

namespace ShortageManager.ClassLibrary.Repositories;

public interface IShortageRepository
{
    void SaveShortages(List<Shortage> shortages);
    List<Shortage>? LoadShortages();
}
