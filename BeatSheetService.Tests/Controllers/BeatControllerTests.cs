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
        var beat = new BeatRequestDto { /* Initialize with test data */ };
        var newBeat = new BeatDto { /* Initialize with test data */ };
        
        _mockService
            .Setup(service => service.Create(It.IsAny<Guid>(), It.IsAny<BeatDto>()))
            .ReturnsAsync((new BeatDto(), new BeatDto()));

        // Act
        var result = await CreateController().Create(beatSheetId, beat);

        // Assert
        var actionResult = Assert.IsType<BeatResponseDto>(result);
        Assert.NotNull(actionResult.Beat);
    }

    [Fact]
    public async Task Update_ReturnsUpdatedBeat()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        var beat = new BeatRequestDto { /* Initialize with test data */ };
        var newBeat = new BeatDto { /* Initialize with test data */ };
        
        _mockService
            .Setup(service => service.Update(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<BeatDto>()))
            .ReturnsAsync((new BeatDto(), new BeatDto()));

        // Act
        var result = await CreateController().Update(beatSheetId, beatId, beat);

        // Assert
        var actionResult = Assert.IsType<BeatResponseDto>(result);
        Assert.NotNull(actionResult.Beat);
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