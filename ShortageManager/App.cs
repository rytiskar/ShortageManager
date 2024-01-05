using ShortageManager.Models;
using ShortageManager.Repositories;

namespace ShortageManager;

public class App
{
    private readonly IShortageRepository _shortageRepository;

    public App(IShortageRepository shortageRepository)
    {
        _shortageRepository = shortageRepository;
    }

    public void Run()
    {
        Shortage shortage1 = new Shortage();
        shortage1.Title = "title";
        shortage1.Name = "name";
        shortage1.Priority = 5;
        shortage1.CreatedOn = DateTime.Now;

        _shortageRepository.SaveShortage(shortage1);

        Shortage shortage2 = new Shortage();
        shortage2.Title = "some";
        shortage2.Name = "shortage";
        shortage2.Priority = 2;
        shortage2.CreatedOn = DateTime.Now;

        _shortageRepository.SaveShortage(shortage2);

        _shortageRepository.DeleteShortage(shortage1);


    }
}