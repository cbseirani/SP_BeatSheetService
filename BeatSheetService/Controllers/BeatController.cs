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
    /// Returns the new beat and the suggested next beat.
    /// </summary>
    [HttpPost]
    public async Task<BeatResponseDto> Create(Guid beatSheetId, [FromBody] BeatDto beat)
    {
        var (updatedBeat, suggestedBeat) = await beatService.Create(beatSheetId, beat);
        return new BeatResponseDto
        {
            Beat = updatedBeat,
            SuggestedNextBeat = suggestedBeat
        };
    }

    /// <summary>
    /// Update a beat in a specific beat sheet.
    /// Returns the updated beat and the suggested next beat.
    /// </summary>
    [HttpPut("{beatId}")]
    public async Task<BeatResponseDto> Update(Guid beatSheetId, Guid beatId, [FromBody] BeatDto beat)
    {
       var (updatedBeat, suggestedBeat) = await beatService.Update(beatSheetId, beatId, beat);
       return new BeatResponseDto
       {
           Beat = updatedBeat,
           SuggestedNextBeat = suggestedBeat
       };
    }
        

    /// <summary>
    /// Delete a beat from a specific beat sheet.
    /// </summary>
    [HttpDelete("{beatId}")]
    public Task Delete(Guid beatSheetId, Guid beatId) => 
        beatService.Delete(beatSheetId, beatId);
}