﻿using BeatSheetService.Common;
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
        var newBeatSheet = new BeatSheetDto { /* create some test data */ };
        _mockService.Setup(service => service.Create(newBeatSheet)).ReturnsAsync(newBeatSheet);

        // Act
        var result = await CreateController().Create(newBeatSheet);

        // Assert
        var actionResult = Assert.IsType<BeatSheetDto>(result);
        Assert.Equal(newBeatSheet, actionResult);
    }

    [Fact]
    public async Task Update_ValidBeatSheetId_ReturnsUpdatedBeatSheet()
    {
        // Arrange
        var beatSheetId = Guid.NewGuid();
        var updatedBeatSheet = new BeatSheetDto { /* create some test data */ };
        _mockService.Setup(service => service.Update(beatSheetId, updatedBeatSheet)).ReturnsAsync(updatedBeatSheet);

        // Act
        var result = await CreateController().Update(beatSheetId, updatedBeatSheet);

        // Assert
        var actionResult = Assert.IsType<BeatSheetDto>(result);
        Assert.Equal(updatedBeatSheet, actionResult);
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