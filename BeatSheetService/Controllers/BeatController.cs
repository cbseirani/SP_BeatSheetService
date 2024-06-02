using BeatSheetService.Common;
using BeatSheetService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeatSheetService.Controllers;

[ApiController]
[Route("beatsheet/{beatSheetId}/beat")]
public class BeatController(IBeatService beatService) : ControllerBase
{
    /// <summary>
    /// Add a beat to a specific beat sheet.
    /// </summary>
    [HttpPost]
    public Task<BeatDto> Create(Guid beatSheetId, [FromBody] BeatDto beat) =>
        beatService.Create(beatSheetId, beat);

    /// <summary>
    /// Update a beat in a specific beat sheet.
    /// </summary>
    [HttpPut("{beatId}")]
    public Task<BeatDto> Update(Guid beatSheetId, Guid beatId, [FromBody] BeatDto beat) =>
        beatService.Update(beatSheetId, beatId, beat);

    /// <summary>
    /// Delete a beat from a specific beat sheet.
    /// </summary>
    [HttpDelete("{beatId}")]
    public void Delete(Guid beatSheetId, Guid beatId) => 
        beatService.Delete(beatSheetId, beatId);
}