using Microsoft.AspNetCore.Mvc;

namespace BeatSheetService.Controllers;

[ApiController]
[Route("beatsheet/{beatSheetId}/beat/{beatId}/act")]
public class ActController : ControllerBase
{
    [HttpPost]
    public IActionResult AddAct(Guid beatSheetId, Guid beatId, [FromBody] int act)
    {
        // Implement logic to add an act to the specified beat
        return Ok();
    }

    [HttpPut("{actId}")]
    public IActionResult UpdateAct(Guid beatSheetId, Guid beatId, Guid actId, [FromBody] int updatedAct)
    {
        // Implement logic to update an act in the specified beat
        return Ok();
    }

    [HttpDelete("{actId}")]
    public IActionResult DeleteAct(Guid beatSheetId, Guid beatId, Guid actId)
    {
        // Implement logic to delete an act from the specified beat
        return Ok();
    }
}