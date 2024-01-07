using ShortageManager.Models;
using ShortageManager.Repositories;
using ShortageManager.Services;
using CommandLine;
using ShortageManager.Commands;
using ConsoleTables;

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

    public void Run(string[] args)
    {

        var result = Parser.Default.ParseArguments<RegisterCommand, ListCommand, DeleteCommand>(args)
            .MapResult(
                (RegisterCommand registerCommand) =>
                {
                    int returnStatus = registerCommand.ExecuteCommand(_shortageService);
                    switch (returnStatus)
                    {
                        case 0:
                            Console.WriteLine("Shortage was added successfully");
                            break;

                        case 1:
                            Console.WriteLine("Shortage was updated");
                            break;

                        case 2:
                            Console.WriteLine("Shortage already exists");
                            break;

                        default:
                            Console.WriteLine("Unexpected return status");
                            break;
                    }
                    return 0; 
                },
                (ListCommand listCommand) =>
                {
                    var shortages = listCommand.ExecuteCommand(_shortageService);
                    DisplayData(shortages);
                    return 0; 
                },
                (DeleteCommand deleteCommand) =>
                {
                    bool noShortagesFound = deleteCommand.ExecuteCommand(_shortageService);
                    if(noShortagesFound)
                    {
                        Console.WriteLine("No request with specified shortage details was found");
                        return 1;
                    }
                    Console.WriteLine("Shortage request was deleted successfully");
                    return 0;
                },
                error =>
                {
                    if(!(args.Contains("--help") || args.Contains("help")))
                    {
                        Console.WriteLine("Invalid arguments");
                        return 1; 
                    }
                    return 0;
                });

    }

    public void DisplayData(List<Shortage>? shortages)
    {
        if (shortages == null || shortages.Count == 0)
        {
            Console.WriteLine("No shortages to display");
            return;
        }

        var table = new ConsoleTable("Title", "Name", "Room", "Category", "Priority", "CreatedOn");

        foreach (var shortage in shortages)
        {
            table.AddRow(shortage.Title, shortage.Name, shortage.Room, shortage.Category, shortage.Priority, shortage.CreatedOn);
        }

        Console.WriteLine(table.ToString());
    }
}