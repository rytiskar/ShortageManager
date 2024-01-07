using Moq;
using ShortageManager.ClassLibrary.Services;
using ShortageManager.ClassLibrary.Repositories;
using ShortageManager.ClassLibrary.Models;
using ShortageManager.ClassLibrary.Enums;

namespace ShortageManagerTests;



public class ShortageServiceTests
{

    private readonly ShortageService _shortageService;
    private readonly Mock<IShortageRepository> _shortageRepoMock = new Mock<IShortageRepository>();

    public ShortageServiceTests()
    {
        _shortageService = new ShortageService(_shortageRepoMock.Object);

    }

    [Fact]
    public void RegisterShortage_ShouldReturnCorrectStatusCode_WhenAddingShortage()
    {
        // Arrange
        var shortages = new List<Shortage>()
        {
            new Shortage
            {
                Title = "title",
                Name = "name",
                Room = RoomType.Kitchen,
                Category = CategoryType.Electronics,
                Priority = 5,
                CreatedOn = DateTime.Now,
                Creator = "user"
            }
        };
        List<Shortage> capturedSavedShortages = null;

        _shortageRepoMock.Setup(repo => repo.LoadShortages()).Returns(shortages);
        _shortageRepoMock.Setup(repo => repo.SaveShortages(It.IsAny<List<Shortage>>()))
            .Callback<List<Shortage>>(savedShortages =>
            {
                capturedSavedShortages = savedShortages;
            });

        // Act
        int statusCode = _shortageService.RegisterShortage("user", "newTitle", "newName", RoomType.Kitchen,
            CategoryType.Electronics, 1);

        // Assert
        Assert.Equal(0, statusCode);
        Assert.Equal(2, capturedSavedShortages.Count());
    }

    [Fact]
    public void RegisterShortage_ShouldReturnCorrectStatusCode_WhenUpdatingExistingShortage()
    {
        // Arrange
        var shortages = new List<Shortage>()
        {
            new Shortage
            {
                Title = "title",
                Name = "name",
                Room = RoomType.Kitchen,
                Category = CategoryType.Electronics,
                Priority = 5,
                CreatedOn = DateTime.Now,
                Creator = "user"
            }
        };
        List<Shortage> capturedSavedShortages = null;

        _shortageRepoMock.Setup(repo => repo.LoadShortages()).Returns(shortages);
        _shortageRepoMock.Setup(repo => repo.SaveShortages(It.IsAny<List<Shortage>>()))
            .Callback<List<Shortage>>(savedShortages =>
            {
                capturedSavedShortages = savedShortages;
            });

        // Act
        int statusCode = _shortageService.RegisterShortage("user", "title", "name", RoomType.Kitchen,
            CategoryType.Electronics, 8);

        // Assert
        Assert.Equal(1, statusCode);
        Assert.Equal(1, capturedSavedShortages.Count());
    }

    [Fact]
    public void RegisterShortage_ShouldReturnCorrectStatusCode_WhenShortageExists()
    {
        // Arrange
        var shortages = new List<Shortage>()
        {
            new Shortage
            {
                Title = "title",
                Name = "name",
                Room = RoomType.Kitchen,
                Category = CategoryType.Electronics,
                Priority = 5,
                CreatedOn = DateTime.Now,
                Creator = "user"
            }
        };
        List<Shortage> capturedSavedShortages = null;

        _shortageRepoMock.Setup(repo => repo.LoadShortages()).Returns(shortages);
        _shortageRepoMock.Setup(repo => repo.SaveShortages(It.IsAny<List<Shortage>>()))
            .Callback<List<Shortage>>(savedShortages =>
            {
                capturedSavedShortages = savedShortages;
            });

        // Act
        int statusCode = _shortageService.RegisterShortage("user", "title", "name", RoomType.Kitchen,
            CategoryType.Electronics, 5);

        // Assert
        Assert.Equal(2, statusCode);
        Assert.Null(capturedSavedShortages);
    }

    [Fact]
    public void ListFilteredShortages_ShouldReturnShortageListForSpecifiedUser_WhenTitleFilterIsApplied()
    {

        // Arrange
        var shortages = new List<Shortage>()
        {
            new Shortage
            {
                Title = "Wireless speakerA",
                Name = "name",
                Room = RoomType.MeetingRoom,
                Category = CategoryType.Electronics,
                Priority = 5,
                CreatedOn = DateTime.Now,
                Creator = "userA"
            },
            new Shortage
            {
                Title = "Snacks",
                Name = "name",
                Room = RoomType.Kitchen,
                Category = CategoryType.Food,
                Priority = 8,
                CreatedOn = DateTime.Now,
                Creator = "userB"
            },
            new Shortage
            {
                Title = "Wireless SpeakerB",
                Name = "name",
                Room = RoomType.Kitchen,
                Category = CategoryType.Electronics,
                Priority = 6,
                CreatedOn = DateTime.Now,
                Creator = "userA"
            },
        };

        _shortageRepoMock.Setup(repo => repo.LoadShortages()).Returns(shortages);

        // Act
        var filteredShortages = _shortageService.ListFilteredShortages("userA", "speaker");

        // Assert
        Assert.NotNull(filteredShortages);
        Assert.Equal(2, filteredShortages.Count());

        Assert.Equal("userA", filteredShortages[0].Creator);
        Assert.Equal("userA", filteredShortages[1].Creator);

        Assert.Equal("Wireless SpeakerB", filteredShortages[0].Title); // Has higher Priority, therefore is first
        Assert.Equal("Wireless speakerA", filteredShortages[1].Title);
    }

    [Fact]
    public void ListFilteredShortages_ShouldReturnAllShortagesForAdmint()
    {

        // Arrange
        var shortages = new List<Shortage>()
        {
            new Shortage
            {
                Title = "Wireless speakerA",
                Name = "name",
                Room = RoomType.MeetingRoom,
                Category = CategoryType.Electronics,
                Priority = 5,
                CreatedOn = DateTime.Now,
                Creator = "userA"
            },
            new Shortage
            {
                Title = "Snacks",
                Name = "name",
                Room = RoomType.Kitchen,
                Category = CategoryType.Food,
                Priority = 8,
                CreatedOn = DateTime.Now,
                Creator = "userB"
            },
            new Shortage
            {
                Title = "Wireless SpeakerB",
                Name = "name",
                Room = RoomType.Kitchen,
                Category = CategoryType.Electronics,
                Priority = 6,
                CreatedOn = DateTime.Now,
                Creator = "userA"
            },
        };

        _shortageRepoMock.Setup(repo => repo.LoadShortages()).Returns(shortages);

        // Act
        var filteredShortages = _shortageService.ListFilteredShortages("admin");

        // Assert
        Assert.NotNull(filteredShortages);
        Assert.Equal(3, filteredShortages.Count());
    }

    [Fact]
    public void DeleteShortage_ShouldReturnFalse_WhenDeletingExistingShortage()
    {
        // Arrange
        var shortages = new List<Shortage>()
        {
            new Shortage
            {
                Title = "Wireless speakerA",
                Name = "name",
                Room = RoomType.MeetingRoom,
                Category = CategoryType.Electronics,
                Priority = 5,
                CreatedOn = DateTime.Now,
                Creator = "userA"
            },
            new Shortage
            {
                Title = "Snacks",
                Name = "name",
                Room = RoomType.Kitchen,
                Category = CategoryType.Food,
                Priority = 8,
                CreatedOn = DateTime.Now,
                Creator = "userB"
            }
        };
        List<Shortage> capturedSavedShortages = null;

        _shortageRepoMock.Setup(repo => repo.LoadShortages()).Returns(shortages);
        _shortageRepoMock.Setup(repo => repo.SaveShortages(It.IsAny<List<Shortage>>()))
            .Callback<List<Shortage>>(savedShortages =>
            {
                capturedSavedShortages = savedShortages;
            });

        // Act
        bool noShortageFound = _shortageService.DeleteShortage("userA", "Wireless speakerA", RoomType.MeetingRoom);

        // Assert
        Assert.False(noShortageFound);
        Assert.Equal(1, capturedSavedShortages.Count());
        Assert.Equal("userB", capturedSavedShortages[0].Creator);
    }

    [Fact]
    public void DeleteShortage_ShouldReturnFalse_WhenDeletingExistingShortageAsAdmin()
    {
        var shortages = new List<Shortage>()
        {
            new Shortage
            {
                Title = "Wireless speakerA",
                Name = "name",
                Room = RoomType.MeetingRoom,
                Category = CategoryType.Electronics,
                Priority = 5,
                CreatedOn = DateTime.Now,
                Creator = "userA"
            },
            new Shortage
            {
                Title = "Snacks",
                Name = "name",
                Room = RoomType.Kitchen,
                Category = CategoryType.Food,
                Priority = 8,
                CreatedOn = DateTime.Now,
                Creator = "userB"
            }
        };
        List<Shortage> capturedSavedShortages = null;

        _shortageRepoMock.Setup(repo => repo.LoadShortages()).Returns(shortages);
        _shortageRepoMock.Setup(repo => repo.SaveShortages(It.IsAny<List<Shortage>>()))
            .Callback<List<Shortage>>(savedShortages =>
            {
                capturedSavedShortages = savedShortages;
            });

        // Act
        bool noShortageFound = _shortageService.DeleteShortage("admin", "Snacks", RoomType.Kitchen);

        // Assert
        Assert.False(noShortageFound);
        Assert.Equal(1, capturedSavedShortages.Count());
        Assert.Equal("userA", capturedSavedShortages[0].Creator);
    }

    [Fact]
    public void DeleteShortage_ShouldReturnTrue_WhenDeletingShortageWithIncorrectDetails()
    {
        var shortages = new List<Shortage>()
        {
            new Shortage
            {
                Title = "Wireless speakerA",
                Name = "name",
                Room = RoomType.MeetingRoom,
                Category = CategoryType.Electronics,
                Priority = 5,
                CreatedOn = DateTime.Now,
                Creator = "userA"
            }
        };
        List<Shortage> capturedSavedShortages = null;

        _shortageRepoMock.Setup(repo => repo.LoadShortages()).Returns(shortages);
        _shortageRepoMock.Setup(repo => repo.SaveShortages(It.IsAny<List<Shortage>>()))
            .Callback<List<Shortage>>(savedShortages =>
            {
                capturedSavedShortages = savedShortages;
            });

        // Act
        bool noShortageFound = _shortageService.DeleteShortage("userA", "Snacks", RoomType.MeetingRoom);

        // Assert
        Assert.True(noShortageFound);
    }
}