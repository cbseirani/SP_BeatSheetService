using BeatSheetService.Common;

namespace BeatSheetService.Services;

public interface IBeatService
{
    Task<BeatDto> Create(Guid beatSheetId, BeatDto beat);
    Task<BeatDto> Update(Guid beatSheetId, Guid beatId, BeatDto beat);
    void Delete(Guid beatSheetId, Guid beatId);
}

public class BeatService(IBeatSheetService beatSheetService, IAiService aiService) : IBeatService
{
    public async Task<BeatDto> Create(Guid beatSheetId, BeatDto beat)
    {
        // validate
        var beatSheet = await beatSheetService.Get(beatSheetId);
        
        // add
        beatSheet.Beats.Add(beat);
        await beatSheetService.Update(beatSheetId, beatSheet);
        
        // get suggested
        beat.SuggestedNextBeat = await aiService.SuggestNextBeat();
        
        return beat;
    }
    
    public async Task<BeatDto> Update(Guid beatSheetId, Guid beatId, BeatDto beat)
    {
        // validate
        var beatSheet = await beatSheetService.Get(beatSheetId);
        var existingBeat = beatSheet.Beats.FirstOrDefault(x => x.Id.Equals(beatId));
        if (existingBeat is null)
            throw new NotFoundException($"Beat {beatId} on beat sheet {beatSheetId} not found!");

        // update
        beatSheet.Beats.Remove(existingBeat);
        beatSheet.Beats.Add(beat);
        await beatSheetService.Update(beatSheetId, beatSheet);

        // get suggested
        beat.SuggestedNextBeat = await aiService.SuggestNextBeat();
        
        return beat;
    }
    
    public async void Delete(Guid beatSheetId, Guid beatId)
    {
        // validate
        var beatSheet = await beatSheetService.Get(beatSheetId);
        var existingBeat = beatSheet.Beats.FirstOrDefault(x => x.Id.Equals(beatId));
        if (existingBeat is null)
            throw new NotFoundException($"Beat {beatId} on beat sheet {beatSheetId} not found!");
        
        // delete
        beatSheet.Beats.Remove(existingBeat);
        await beatSheetService.Update(beatSheetId, beatSheet);
    }
}