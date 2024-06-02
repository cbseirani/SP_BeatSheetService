using BeatSheetService.Common;

namespace BeatSheetService.Services;

public interface IAiService
{
    Task<BeatDto> SuggestNextBeat();
    Task<ActDto> SuggestNextAct();
}

public class AiService : IAiService
{
    public async Task<BeatDto> SuggestNextBeat()
    {
        return new BeatDto();
    }

    public async Task<ActDto> SuggestNextAct()
    {
        return new ActDto();
    }
}