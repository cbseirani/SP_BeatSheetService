using BeatSheetService.Common;
using BeatSheetService.Services.Ai;

namespace BeatSheetService.Tests.Services;

public class AiServiceTests
{
    [Fact]
    public async Task SuggestNextBeat_ReturnsValidSuggestion()
    {
        // Arrange
        var currentBeat = new BeatDto { Description = "2" };
        var beats = new List<BeatDto>
        {
            new () { Description = "1" },
            new () { Description = "7" },
            new () { Description = "5" },
            new () { Description = "bart" },
            new () { Description = "chicken" },
            currentBeat
        };
        
        // Act
        var suggestion = await CreateService().SuggestNextBeat(beats, currentBeat);

        // Assert
        Assert.NotNull(suggestion);
    }

    [Fact]
    public async Task SuggestNextAct_ReturnsValidSuggestion()
    {
        // Arrange
        var currentAct = new ActDto { Description = "2", Duration = 2, CameraAngle = "left" };
        var acts = new List<ActDto>
        {
            new () { Description = "1", Duration = 1, CameraAngle = "left" },
            new () { Description = "2", Duration = 2, CameraAngle = "right"},
            new () { Description = "3", Duration = 3, CameraAngle = "left" },
            new () { Description = "4", Duration = 4, CameraAngle = "right" },
            new () { Description = "5", Duration = 5, CameraAngle = "left"},
            currentAct
        };
        
        // Act
        var suggestion = await CreateService().SuggestNextAct(acts, currentAct);
        
        // Assert
        Assert.NotNull(suggestion);
    }
    
    private AiService CreateService() => new ();
}