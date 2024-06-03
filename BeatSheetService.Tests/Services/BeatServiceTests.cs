using BeatSheetService.Common;
using BeatSheetService.Services;
using BeatSheetService.Services.Ai;
using Microsoft.Extensions.Logging;
using Moq;

namespace BeatSheetService.Tests.Services;

public class BeatServiceTests
{
    // Mock dependencies
    private readonly Mock<IBeatSheetService> _mockBeatSheetService = new();
    private readonly Mock<IAiService> _mockAiService = new();
    private readonly Mock<ILogger<BeatService>> _mockLogger = new();

    [Fact]
    public async Task Get_ReturnsCorrectData()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        var expectedBeat = new BeatDto { Id = beatId.ToString() };
        var beatSheet = new BeatSheetDto { Beats = new List<BeatDto> { expectedBeat } };
        _mockBeatSheetService.Setup(s => s.Get(beatSheetId)).ReturnsAsync(beatSheet);

        // Act
        var (resultBeatSheet, resultBeat) = await CreateService().Get(beatSheetId, beatId);

        // Assert
        Assert.NotNull(resultBeatSheet);
        Assert.NotNull(resultBeat);
        Assert.Equal(expectedBeat, resultBeat);
    }

    [Fact]
    public async Task Create_AddsBeatAndReturnsIt()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beat = new BeatDto { Id = Guid.NewGuid().ToString(), Description = "testbeat" };
        var beatSheet = new BeatSheetDto
        {
            Id = beatSheetId.ToString(),
            Beats = new List<BeatDto> { beat }
        };
        
        _mockBeatSheetService
            .Setup(s => s.Get(It.IsAny<Guid>()))
            .ReturnsAsync(beatSheet);

        _mockBeatSheetService
            .Setup(s => s.Update(It.IsAny<Guid>(), It.IsAny<BeatSheetDto>()));
        
        _mockAiService
            .Setup(s => s.SuggestNextBeat(It.IsAny<List<BeatDto>>(), It.IsAny<BeatDto>()))
            .ReturnsAsync(beat);

        // Act
        var result = await CreateService().Create(beatSheetId, beat);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(beatSheet.Beats);
        Assert.Contains(beat, beatSheet.Beats);
    }

    [Fact]
    public async Task Update_ReplacesBeatAndReturnsUpdatedBeat()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        var beat = new BeatDto { Id = beatId.ToString() };
        var existingBeat = new BeatDto { Id = beatId.ToString() };
        var beatSheet = new BeatSheetDto { Id = beatSheetId.ToString(), Beats = new List<BeatDto> { existingBeat } };
        
        _mockBeatSheetService
            .Setup(s => s.Get(It.IsAny<Guid>()))
            .ReturnsAsync(beatSheet);

        _mockBeatSheetService
            .Setup(s => s.Update(It.IsAny<Guid>(), It.IsAny<BeatSheetDto>()));
        
        _mockAiService
            .Setup(s => s.SuggestNextBeat(It.IsAny<List<BeatDto>>(), It.IsAny<BeatDto>()))
            .ReturnsAsync(beat);

        // Act
        var result = await CreateService().Update(beatSheetId, beatId, beat);

        // Assert
        Assert.NotNull(result);
        Assert.DoesNotContain(existingBeat, beatSheet.Beats);
        Assert.Contains(beat, beatSheet.Beats);
    }

    [Fact]
    public async Task Delete_RemovesBeat()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        var existingBeat = new BeatDto { Id = beatId.ToString() };
        var beatSheet = new BeatSheetDto { Beats = new List<BeatDto> { existingBeat } };
        _mockBeatSheetService.Setup(s => s.Get(beatSheetId)).ReturnsAsync(beatSheet);

        // Act
        await CreateService().Delete(beatSheetId, beatId);

        // Assert
        Assert.DoesNotContain(existingBeat, beatSheet.Beats);
    }
    
    private BeatService CreateService() => new (_mockBeatSheetService.Object, _mockAiService.Object, _mockLogger.Object);
}