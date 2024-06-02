using BeatSheetService.Common;
using BeatSheetService.Services.Ai;
using Microsoft.Extensions.Logging;

namespace BeatSheetService.Services;

public interface IActService
{
    Task<(BeatSheetDto, BeatDto, ActDto)> Get(Guid beatSheetId, Guid beatId, Guid actId);
    Task<ActDto> Create(Guid beatSheetId, Guid beatId, ActDto act);
    Task<ActDto> Update(Guid beatSheetId, Guid beatId, Guid actId, ActDto act);
    Task Delete(Guid beatSheetId, Guid beatId, Guid actId);
}

public class ActService(IBeatService beatService, IAiService aiService, ILogger<ActService> logger) : IActService
{
    public async Task<(BeatSheetDto, BeatDto, ActDto)> Get(Guid beatSheetId, Guid beatId, Guid actId)
    {
        logger.LogInformation($"Getting act {actId}");
        var (beatSheet, beat) = await beatService.Get(beatSheetId, beatId);
        var act = beat.Acts.FirstOrDefault(x => x.Id.Equals(actId));
        if (act is null)
            throw new NotFoundException($"Act {actId} on beat {beatId} on beat sheet {beatSheetId} not found!");

        return (beatSheet, beat, act);
    }
    
    public async Task<ActDto> Create(Guid beatSheetId, Guid beatId, ActDto act)
    {
        var (beatSheet, beat) = await beatService.Get(beatSheetId, beatId);

        logger.LogInformation($"Creating act");
        act.Id = Guid.NewGuid();
        
        return await AddActAndSuggestNextAct(beatSheet, beat, act);
    }
    
    public async Task<ActDto> Update(Guid beatSheetId, Guid beatId, Guid actId, ActDto act)
    {
        var (beatSheet, beat, existingAct) = await Get(beatSheetId, beatId, actId);

        logger.LogInformation($"Updating act {actId}");
        beat.Acts.Remove(existingAct);

        return await AddActAndSuggestNextAct(beatSheet, beat, act);
    }
    
    public async Task Delete(Guid beatSheetId, Guid beatId, Guid actId)
    {
        var (beatSheet, beat, existingAct) = await Get(beatSheetId, beatId, actId);
        
        logger.LogInformation($"Deleting act {actId}");
        beat.Acts.Remove(existingAct);
        await beatService.Update(beatSheet.Id, beat.Id, beat);
    }

    private async Task<ActDto> AddActAndSuggestNextAct(BeatSheetDto beatSheet, BeatDto beat, ActDto act)
    {
        act.Timestamp ??= DateTimeOffset.UtcNow;
        beat.Acts.Add(act);
        await beatService.Update(beatSheet.Id, beat.Id, beat);
        
        logger.LogInformation("Suggesting next act");
        act.SuggestedNextAct = await aiService.SuggestNextAct(beat.Acts, act);

        return act;
    }
}