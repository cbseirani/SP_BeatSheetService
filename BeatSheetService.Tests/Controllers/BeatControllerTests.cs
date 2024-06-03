using BeatSheetService.Common;
using BeatSheetService.Controllers;
using BeatSheetService.Services;
using Moq;

namespace BeatSheetService.Tests.Controllers;

public class BeatControllerTests
{
    private readonly Mock<IBeatService> _mockService = new();

    [Fact]
    public async Task Create_ReturnsNewBeat()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beat = new BeatDto { /* Initialize with test data */ };
        _mockService.Setup(service => service.Create(beatSheetId, beat)).ReturnsAsync(beat);

        // Act
        var result = await CreateController().Create(beatSheetId, beat);

        // Assert
        var actionResult = Assert.IsType<BeatDto>(result);
        Assert.Equal(beat, actionResult);
    }

    [Fact]
    public async Task Update_ReturnsUpdatedBeat()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        var beat = new BeatDto { /* Initialize with test data */ };
        _mockService.Setup(service => service.Update(beatSheetId, beatId, beat)).ReturnsAsync(beat);

        // Act
        var result = await CreateController().Update(beatSheetId, beatId, beat);

        // Assert
        var actionResult = Assert.IsType<BeatDto>(result);
        Assert.Equal(beat, actionResult);
    }

    [Fact]
    public async Task Delete_CallsDeleteMethod()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        _mockService.Setup(service => service.Delete(beatSheetId, beatId)).Verifiable("Delete was not called");

        // Act
        await CreateController().Delete(beatSheetId, beatId);

        // Assert
        _mockService.Verify();
    }
    
    private BeatController CreateController() => new(_mockService.Object);
}