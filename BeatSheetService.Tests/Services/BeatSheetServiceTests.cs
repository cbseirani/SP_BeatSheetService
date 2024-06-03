using BeatSheetService.Common;
using BeatSheetService.Repositories;
using BeatSheetService.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace BeatSheetService.Tests.Services;

public class BeatSheetServiceTests
{
    private readonly Mock<IBeatSheetRepository> _mockRepository = new();
    private readonly Mock<ILogger<BeatSheetServiceX>> _mockLogger = new();
    
    [Fact]
    public async Task List_ReturnsListOfBeatSheets()
    {
        // Arrange
        var expectedBeatSheets = new List<BeatSheetDto> { /* create some test data */ };
        _mockRepository.Setup(repo => repo.List()).ReturnsAsync(expectedBeatSheets);

        // Act
        var result = await CreateService().List();

        // Assert
        Assert.Equal(expectedBeatSheets, result);
    }

    [Fact]
    public async Task Get_ValidBeatSheetId_ReturnsBeatSheet()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var expectedBeatSheet = new BeatSheetDto { Id = beatSheetId.ToString() };

        _mockRepository.Setup(repo => repo.Get(beatSheetId)).ReturnsAsync(expectedBeatSheet);

        // Act
        var result = await CreateService().Get(beatSheetId);

        // Assert
        Assert.Equal(expectedBeatSheet, result);
    }

    [Fact]
    public async Task Get_InvalidBeatSheetId_ThrowsNotFoundException()
    {
        // Arrange
        var invalidBeatSheetId = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.Get(invalidBeatSheetId)).ReturnsAsync((BeatSheetDto)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => CreateService().Get(invalidBeatSheetId));
    }

    [Fact]
    public async Task Create_ValidBeatSheet_CreatesAndReturnsBeatSheet()
    {
        // Arrange
        var newBeatSheet = new BeatSheetDto { /* create some test data */ };
        _mockRepository.Setup(repo => repo.Create(newBeatSheet)).ReturnsAsync(newBeatSheet);

        // Act
        var result = await CreateService().Create(newBeatSheet);

        // Assert
        Assert.Equal(newBeatSheet, result);
    }

    private BeatSheetServiceX CreateService() => new (_mockRepository.Object, _mockLogger.Object);
}