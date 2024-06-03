using BeatSheetService.Common;
using BeatSheetService.Services.Ai;
using Microsoft.Extensions.Logging;

namespace BeatSheetService.Services;

public interface IBeatService
{
    Task<(BeatSheetDto, BeatDto)> Get(Guid beatSheetId, Guid beatId);
    Task<(BeatDto, BeatDto?)> Create(Guid beatSheetId, BeatDto beat);
    Task<(BeatDto, BeatDto?)> Update(Guid beatSheetId, Guid beatId, BeatDto beat);
    Task Delete(Guid beatSheetId, Guid beatId);
}

public class BeatService(IBeatSheetService beatSheetService, IAiService aiService, ILogger<BeatService> logger) : IBeatService
{
    public async Task<(BeatSheetDto, BeatDto)> Get(Guid beatSheetId, Guid beatId)
    {
        logger.LogInformation($"Getting beat {beatId}");
        var beatSheet = await beatSheetService.Get(beatSheetId);
        var beat = beatSheet.Beats.FirstOrDefault(x => x.Id.Equals(beatId.ToString()));
        if (beat is null)
            throw new NotFoundException($"Beat {beatId} on beat sheet {beatSheetId} not found!");

        return (beatSheet, beat);
    }
    
    public async Task<(BeatDto, BeatDto?)> Create(Guid beatSheetId, BeatDto beat)
    {
        var beatSheet = await beatSheetService.Get(beatSheetId);
        
        logger.LogInformation("Creating beat");
        beat.Id = Guid.NewGuid().ToString();
        beat.Acts = new List<ActDto>();
        return await AddBeatAndSuggestNextBeat(beatSheet, beat);
    }
    
    public async Task<(BeatDto, BeatDto?)> Update(Guid beatSheetId, Guid beatId, BeatDto beat)
    {
        var (beatSheet, existingBeat) = await Get(beatSheetId, beatId);
        
        logger.LogInformation($"Updating beat {beatId}");
        beatSheet.Beats.Remove(existingBeat);
        beat.Id = existingBeat.Id;
        return await AddBeatAndSuggestNextBeat(beatSheet, beat);
    }
    
    public async Task Delete(Guid beatSheetId, Guid beatId)
    {
        var (beatSheet, existingBeat) = await Get(beatSheetId, beatId);
        
        logger.LogInformation($"Deleting beat {beatId}");
        beatSheet.Beats.Remove(existingBeat);
        await beatSheetService.Update(beatSheetId, beatSheet);
    }

    private async Task<(BeatDto, BeatDto?)> AddBeatAndSuggestNextBeat(BeatSheetDto beatSheet, BeatDto beat)
    {
        beat.Timestamp = DateTimeOffset.UtcNow;
        beatSheet.Beats.Add(beat);
        await beatSheetService.Update(Guid.Parse(beatSheet.Id), beatSheet);
        
        logger.LogInformation("Suggesting next beat");
        var suggestedNextBeat = await aiService.SuggestNextBeat(beatSheet.Beats, beat);
        
        return (beat, suggestedNextBeat);
    }
}