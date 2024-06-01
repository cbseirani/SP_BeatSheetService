using Microsoft.AspNetCore.Mvc;
using BeatSheetService.Services;

namespace BeatSheetService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeatSheetController(IBeatSheetService beatSheetService) : ControllerBase
    {
        private readonly IBeatSheetService _beatSheetService = beatSheetService;

        [HttpPost]
        public IActionResult Create([FromBody] int beatSheet) => Ok();

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] int updatedBeatSheet)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok();
        }
    }
}