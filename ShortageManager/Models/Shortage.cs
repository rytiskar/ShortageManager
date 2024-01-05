using ShortageManager.Enums;

namespace ShortageManager.Models;

public class Shortage
{
    public string Title { get; set; }
    public string Name { get; set; }
    public RoomType Room { get; set; }
    public CategoryType Category { get; set; }
    public int Priority { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Creator { get; set; }
}
