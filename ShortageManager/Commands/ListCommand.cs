using CommandLine;
using ShortageManager.Models;
using ShortageManager.Services;

namespace ShortageManager.Commands;

[Verb("list", HelpText = "List shortage requests with filtering options")]
public class ListCommand
{
    [Value(0, MetaName = "User", Required = true, HelpText = "Creator of shortage")]
    public string User { get; set; }

    [Option('t', "title", HelpText = "Filter by title")]
    public string TitleFilter { get; set; }

    [Option('s', "createdonstart", HelpText = "Filter by CreatedOn start date")]
    public string CreatedOnStart { get; set; }

    [Option('e', "createdonend", HelpText = "Filter by CreatedOn end date")]
    public string CreatedOnEnd { get; set; }

    [Option('c', "category", HelpText = "Filter by category")]
    public string CategoryFilter { get; set; }

    [Option('r', "room", HelpText = "Filter by room")]
    public string RoomFilter { get; set; }

    public List<Shortage>? ExecuteCommand(IShortageService shortageService)
    {
        DateTime? createdOnStart = null;
        DateTime? createdOnEnd = null;

        if (DateTime.TryParse(CreatedOnStart, out var start) && (DateTime.TryParse(CreatedOnEnd, out var end)))
        {
            createdOnStart = start;
            createdOnEnd = end;
        }

        return shortageService.ListFilteredShortages(User, TitleFilter, 
            createdOnStart, createdOnEnd, CategoryFilter, RoomFilter);
    }

}
