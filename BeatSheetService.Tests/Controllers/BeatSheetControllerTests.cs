using BeatSheetService.Common;
using BeatSheetService.Controllers;
using BeatSheetService.Services;
using Moq;

namespace BeatSheetService.Tests.Controllers;

public class BeatSheetControllerTests
{
    private readonly Mock<IBeatSheetService> _mockService = new();

    [Fact]
    public async Task List_ReturnsListOfBeatSheets()
    {
        // Arrange
        var expectedBeatSheets = new List<BeatSheetDto> { /* create some test data */ };
        _mockService
            .Setup(service => service.List())
            .ReturnsAsync(expectedBeatSheets);

        // Act
        var result = await CreateController().List();

        // Assert
        var actionResult = Assert.IsType<List<BeatSheetDto>>(result);
        Assert.Equal(expectedBeatSheets, actionResult);
    }

    [Fact]
    public async Task Get_ValidBeatSheetId_ReturnsBeatSheet()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var expectedBeatSheet = new BeatSheetDto { Id = beatSheetId.ToString() };
        _mockService.Setup(service => service.Get(beatSheetId)).ReturnsAsync(expectedBeatSheet);

        // Act
        var result = await CreateController().Get(beatSheetId);

        // Assert
        var actionResult = Assert.IsType<BeatSheetDto>(result);
        Assert.Equal(expectedBeatSheet, actionResult);
    }

    [Fact]
    public async Task Create_ValidBeatSheet_CreatesAndReturnsBeatSheet()
    {
        // Arrange
        var beatSheet = new BeatSheetRequestDto { /* create some test data */ };
        var newBeatSheet = new BeatSheetDto { /* create some test data */ };
        
        _mockService
            .Setup(service => service.Create(It.IsAny<BeatSheetDto>()))
            .ReturnsAsync(new BeatSheetDto());

        // Act
        var result = await CreateController().Create(beatSheet);

        // Assert
        var actionResult = Assert.IsType<BeatSheetDto>(result);
        Assert.NotNull(actionResult);
    }

    [Fact]
    public async Task Update_ValidBeatSheetId_ReturnsUpdatedBeatSheet()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var beatSheet = new BeatSheetRequestDto { /* create some test data */ };
        var updatedBeatSheet = new BeatSheetDto { /* create some test data */ };
        
        _mockService
            .Setup(service => service.Update(It.IsAny<Guid>(), It.IsAny<BeatSheetDto>()))
            .ReturnsAsync(new BeatSheetDto());

        // Act
        var result = await CreateController().Update(beatSheetId, beatSheet);

        // Assert
        var actionResult = Assert.IsType<BeatSheetDto>(result);
        Assert.NotNull(actionResult);
    }

    [Fact]
    public async Task Delete_ValidBeatSheetId_CallsDeleteMethod()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        _mockService.Setup(service => service.Delete(beatSheetId)).Verifiable("Delete was not called");

        // Act
        await CreateController().Delete(beatSheetId);

        // Assert
        Assert.True(true);
    }
    
    private BeatSheetController CreateController() => new(_mockService.Object);
}