using BeatSheetService.Common;
using BeatSheetService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeatSheetService.Controllers;

[ApiController]
[Route("beatsheet/{beatSheetId}/beat/{beatId}/act")]
public class ActController(IActService actService) : ControllerBase
{
    /// <summary>
    /// Add an act to a specific beat.
    /// Returns the new act and the suggested next act.
    /// </summary>
    [HttpPost]
    public async Task<ActResponseDto> Create(Guid beatSheetId, Guid beatId, [FromBody] ActDto act)
    {
        var (newAct, suggestedAct) = await actService.Create(beatSheetId, beatId, act);
        return new ActResponseDto
        {
            Act = newAct,
            SuggestedNextAct = suggestedAct
        };
    }

    /// <summary>
    /// Update an act in a specific beat.
    /// Returns the updated act and the suggested next act.
    /// </summary>
    [HttpPut("{actId}")]
    public async Task<ActResponseDto> Update(Guid beatSheetId, Guid beatId, Guid actId, [FromBody] ActDto act)
    {
        var (updatedAct, suggestedAct) = await actService.Update(beatSheetId, beatId, actId, act);
        return new ActResponseDto()
        {
            Act = updatedAct,
            SuggestedNextAct = suggestedAct
        };
    }

    /// <summary>
    /// Delete an act from a specific beat.
    /// </summary>
    [HttpDelete("{actId}")]
    public Task Delete(Guid beatSheetId, Guid beatId, Guid actId) => 
        actService.Delete(beatSheetId, beatId, actId);
}