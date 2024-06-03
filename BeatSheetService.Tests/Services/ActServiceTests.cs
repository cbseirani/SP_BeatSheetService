using BeatSheetService.Common;
using BeatSheetService.Services;
using BeatSheetService.Services.Ai;
using Microsoft.Extensions.Logging;
using Moq;

namespace BeatSheetService.Tests.Services;

public class ActServiceTests
{
    // Mock dependencies
    private readonly Mock<IBeatService> _mockBeatService = new();
    private readonly Mock<IAiService> _mockAiService = new();
    private readonly Mock<ILogger<ActService>> _mockLogger = new();

    [Fact]
    public async Task Get_ReturnsCorrectData()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        var actId = Guid.NewGuid();
        var expectedAct = new ActDto { Id = actId.ToString() };
        var beat = new BeatDto { Acts = new List<ActDto> { expectedAct } };
        _mockBeatService.Setup(s => s.Get(beatSheetId, beatId)).ReturnsAsync((new BeatSheetDto(), beat));

        // Act
        var result = await CreateService().Get(beatSheetId, beatId, actId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedAct, result.Item3);
    }

    [Fact]
    public async Task Create_AddsActAndReturnsIt()
    {
        // Arrange
        var actId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        var newAct = new ActDto { Id = actId.ToString(), Description = "act1" };
        var beatSheetId = Guid.NewGuid();
        var beat = new BeatDto
        {
            Id = beatId.ToString(), 
            Description = "testbeat",
            Acts = new List<ActDto> { newAct }
        };
        var beatSheet = new BeatSheetDto
        {
            Id = beatSheetId.ToString(),
            Beats = new List<BeatDto> { beat }
        };
        
        _mockBeatService
            .Setup(s => s.Get(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync((beatSheet, beat));
        
        _mockBeatService
            .Setup(s => s.Update(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<BeatDto>()));
        
        _mockAiService
            .Setup(s => s.SuggestNextAct(It.IsAny<List<ActDto>>(), It.IsAny<ActDto>()))
            .ReturnsAsync(newAct);

        // Act
        var result = await CreateService().Create(beatSheetId, beatId, newAct);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(beat.Acts);
        Assert.Contains(newAct, beat.Acts);
    }

    [Fact]
    public async Task Update_ReplacesActAndReturnsUpdatedAct()
    {
        // Arrange
        var actId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        var existingAct = new ActDto { Id = actId.ToString(), Description = "act1" };
        var updatedAct = new ActDto { Id = actId.ToString(), Description = "act2" };
        var beatSheetId = Guid.NewGuid();
        var beat = new BeatDto
        {
            Id = beatId.ToString(), 
            Description = "testbeat",
            Acts = new List<ActDto> { existingAct }
        };
        var beatSheet = new BeatSheetDto
        {
            Id = beatSheetId.ToString(),
            Beats = new List<BeatDto> { beat }
        };
        
        _mockBeatService
            .Setup(s => s.Get(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync((beatSheet, beat));

        _mockBeatService
            .Setup(s => s.Update(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<BeatDto>()));
        
        _mockAiService
            .Setup(s => s.SuggestNextAct(It.IsAny<List<ActDto>>(), It.IsAny<ActDto>()))
            .ReturnsAsync(updatedAct);

        // Act
        var result = await CreateService().Update(beatSheetId, beatId, actId, updatedAct);

        // Assert
        Assert.NotNull(result);
        Assert.DoesNotContain(existingAct, beat.Acts);
        Assert.Contains(updatedAct, beat.Acts);
    }

    [Fact]
    public async Task Delete_RemovesAct()
    {
        // Arrange
        var actId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        var existingAct = new ActDto { Id = actId.ToString() };
        var beatSheetId = Guid.NewGuid();
        var beat = new BeatDto
        {
            Id = beatId.ToString(), 
            Description = "testbeat",
            Acts = new List<ActDto> { existingAct }
        };
        var beatSheet = new BeatSheetDto
        {
            Id = beatSheetId.ToString(),
            Beats = new List<BeatDto> { beat }
        };
        
        _mockBeatService
            .Setup(s => s.Get(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync((beatSheet, beat));

        _mockBeatService
            .Setup(s => s.Update(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<BeatDto>()));

        // Act
        await CreateService().Delete(beatSheetId, beatId, actId);

        // Assert
        Assert.DoesNotContain(existingAct, beat.Acts);
    }
    
    private ActService CreateService() => new (_mockBeatService.Object, _mockAiService.Object, _mockLogger.Object);
}