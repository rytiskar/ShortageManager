using CommandLine;
using ShortageManager.Enums;
using ShortageManager.Services;

namespace ShortageManager.Commands;

[Verb("register", HelpText = "Register a new shortage")]
public class RegisterCommand
{
    [Value(0, MetaName = "User", Required = true, HelpText = "Creator of shortage")]
    public string User { get; set; }

    [Value(1, MetaName = "Title", Required = true, HelpText = "Title of shortage")]
    public string Title { get; set; }

    [Value(2, MetaName = "Name", Required = true, HelpText = "Name of shortage")]
    public string Name { get; set; }

    [Value(3, MetaName = "Room", Required = true, HelpText = "Room where the shortage is located (MeetingRoom / kitchen / bathroom)")]
    public RoomType Room { get; set; }

    [Value(4, MetaName = "Category", Required = true, HelpText = "Category of the shortage (Electronics / Food / Other)")]
    public CategoryType Category { get; set; }

    [Value(5, MetaName = "Priority", Default = 1, HelpText = "Priority of the shortage (1 - not important, 10 - very important)")]
    public int Priority { get; set; }

    public int ExecuteCommand(IShortageService shortageService)
    {
        return shortageService.RegisterShortage(User, Title, Name, Room, Category, Priority);
    }

}