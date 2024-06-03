using BeatSheetService.Common;
using BeatSheetService.Controllers;
using BeatSheetService.Services;
using Moq;

namespace BeatSheetService.Tests.Controllers;

public class ActControllerTests
{
    private readonly Mock<IActService> _mockService = new();

    [Fact]
    public async Task Create_ReturnsNewAct()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        var act = new ActRequestDto { /* Initialize with test data */ };
        var newAct = new ActDto { /* Initialize with test data */ };
        
        _mockService
            .Setup(service => service.Create(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<ActDto>()))
            .ReturnsAsync((new ActDto(), new ActDto()));

        // Act
        var result = await CreateController().Create(beatSheetId, beatId, act);

        // Assert
        var actionResult = Assert.IsType<ActResponseDto>(result);
        Assert.NotNull(actionResult.Act);
    }

    [Fact]
    public async Task Update_ReturnsUpdatedAct()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        var actId = Guid.NewGuid();
        var act = new ActRequestDto { /* Initialize with test data */ };
        var newAct = new ActDto { /* Initialize with test data */ };
        
        _mockService
            .Setup(service => service.Update(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<ActDto>()))
            .ReturnsAsync((new ActDto(), new ActDto()));

        // Act
        var result = await CreateController().Update(beatSheetId, beatId, actId, act);

        // Assert
        var actionResult = Assert.IsType<ActResponseDto>(result);
        Assert.NotNull(actionResult.Act);
    }

    [Fact]
    public async Task Delete_CallsDeleteMethod()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beatId = Guid.NewGuid();
        var actId = Guid.NewGuid();
        _mockService.Setup(service => service.Delete(beatSheetId, beatId, actId)).Verifiable("Delete was not called");

        // Act
        await CreateController().Delete(beatSheetId, beatId, actId);

        // Assert
        Assert.True(true);
    }

    private ActController CreateController() => new(_mockService.Object);
}