using CommandLine;
using ShortageManager.Enums;
using ShortageManager.Services;

namespace ShortageManager.Commands;

[Verb("delete", HelpText = "Delete a shortage")]
public class DeleteCommand
{
    [Value(0, MetaName = "User", Required = true, HelpText = "Creator of shortage")]
    public string User { get; set; }

    [Value(1, MetaName = "Title", Required = true, HelpText = "Title of shortage")]
    public string Title { get; set; }

    [Value(3, MetaName = "Room", Required = true, HelpText = "Room where the shortage is located (MeetingRoom / kitchen / bathroom)")]
    public RoomType Room { get; set; }

    public bool ExecuteCommand(IShortageService shortageService)
    {
        return shortageService.DeleteShortage(User, Title, Room); 
    }
}
