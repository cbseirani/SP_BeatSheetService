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
    /// </summary>
    [HttpPost]
    public Task<ActDto> Create(Guid beatSheetId, Guid beatId, [FromBody] ActDto act) =>
        actService.Create(beatSheetId, beatId, act);
    
    /// <summary>
    /// Update an act in a specific beat.
    /// </summary>
    [HttpPut("{actId}")]
    public Task<ActDto> Update(Guid beatSheetId, Guid beatId, Guid actId, [FromBody] ActDto act) =>
        actService.Update(beatSheetId, beatId, actId, act);

    /// <summary>
    /// Delete an act from a specific beat.
    /// </summary>
    [HttpDelete("{actId}")]
    public void Delete(Guid beatSheetId, Guid beatId, Guid actId) => 
        actService.Delete(beatSheetId, beatId, actId);
}