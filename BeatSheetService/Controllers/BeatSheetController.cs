using BeatSheetService.Common;
using Microsoft.AspNetCore.Mvc;
using BeatSheetService.Services;
using Mapster;

namespace BeatSheetService.Controllers;

[ApiController]
[Route("beatsheet")]
public class BeatSheetController(IBeatSheetService beatSheetService) : ControllerBase
{
    /// <summary>
    /// List all beat sheets.
    /// </summary>
    [HttpGet]
    public Task<IEnumerable<BeatSheetDto>> List() => 
        beatSheetService.List();

    /// <summary>
    /// Retrieve a beat sheet by its ID.
    /// </summary>
    [HttpGet("{beatSheetId:guid}")]
    public Task<BeatSheetDto> Get(Guid beatSheetId) => 
        beatSheetService.Get(beatSheetId);
    
    /// <summary>
    /// Create a new beat sheet. Returns the new beat sheet.
    /// </summary>
    [HttpPost]
    public Task<BeatSheetDto> Create([FromBody] BeatSheetRequestDto beatSheet) => 
        beatSheetService.Create(beatSheet.Adapt<BeatSheetDto>());

    /// <summary>
    /// Update a beat sheet by its ID. Returns the updated beat sheet.
    /// </summary>
    [HttpPut("{beatSheetId:guid}")]
    public Task<BeatSheetDto> Update(Guid beatSheetId, [FromBody] BeatSheetRequestDto beatSheet) =>
        beatSheetService.Update(beatSheetId, beatSheet.Adapt<BeatSheetDto>());

    /// <summary>
    /// Delete a beat sheet by its ID.
    /// </summary>
    [HttpDelete("{beatSheetId:guid}")]
    public Task Delete(Guid beatSheetId) => 
        beatSheetService.Delete(beatSheetId);
}