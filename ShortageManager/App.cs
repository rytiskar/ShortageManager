using ShortageManager.Models;
using ShortageManager.Repositories;
using ShortageManager.Services;

namespace ShortageManager;

public class App
{
    private readonly IShortageRepository _shortageRepository;
    private readonly IShortageService _shortageService;

    public App(IShortageRepository shortageRepository, IShortageService shortageService)
    {
        _shortageRepository = shortageRepository;
        _shortageService = shortageService;
    }

    public void Run()
    {
        Shortage shortage1 = new Shortage();
        shortage1.Title = "titleA";
        shortage1.Name = "name";
        shortage1.Priority = 5;
        shortage1.CreatedOn = DateTime.Now;
        shortage1.Room = (Enums.RoomType)1;
        shortage1.Creator = "user";

        _shortageRepository.SaveShortage(shortage1);
        
        Shortage shortage2 = new Shortage();
        shortage2.Title = "titleB";
        shortage2.Name = "name";
        shortage2.Priority = 10;
        shortage2.CreatedOn = DateTime.Now;
        shortage2.Room = (Enums.RoomType)1;
        shortage2.Creator = "user";

        _shortageRepository.SaveShortage(shortage2);

        Shortage shortage3 = new Shortage();
        shortage3.Title = "shortage";
        shortage3.Name = "shortageName";
        shortage3.Priority = 2;
        shortage3.CreatedOn = DateTime.Now;
        shortage3.Room = (Enums.RoomType)2;
        shortage3.Creator = "user2";

        _shortageRepository.SaveShortage(shortage3);

        var filteredShortages = _shortageService.FilterShortages("manager", null, null, null, null, null);
        
        foreach(var shortage in filteredShortages)
        {
            Console.WriteLine(shortage.Title);
            Console.WriteLine(shortage.Name);
            Console.WriteLine(shortage.Creator);
            /*Console.WriteLine(shortage.Room.ToString());*/
            Console.WriteLine("");
        }

        /*_shortageRepository.DeleteShortage(shortage1);*/




    }
}