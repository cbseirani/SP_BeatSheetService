using BeatSheetService.Common;
using BeatSheetService.Repositories;

namespace BeatSheetService.Services;

public interface IBeatSheetService
{
    Task<IEnumerable<BeatSheetDto>> List();
    Task<BeatSheetDto> Get(Guid beatSheetId);
    Task<BeatSheetDto> Create(BeatSheetDto beatSheet);
    Task<BeatSheetDto> Update(Guid beatSheetId, BeatSheetDto beatSheet);
    void Delete(Guid beatSheetId);
}

public class BeatSheetService(IBeatSheetRepository beatSheetRepository) : IBeatSheetService
{
    public Task<IEnumerable<BeatSheetDto>> List() => beatSheetRepository.List();
    
    public async Task<BeatSheetDto> Get(Guid beatSheetId)
    {
        var beatSheet = await beatSheetRepository.Get(beatSheetId);
        if (beatSheet is null)
            throw new NotFoundException($"Beat sheet {beatSheetId} not found!");
        return beatSheet;
    }

    public Task<BeatSheetDto> Create(BeatSheetDto beatSheet) => 
        beatSheetRepository.Create(beatSheet);
    
    public async Task<BeatSheetDto> Update(Guid beatSheetId, BeatSheetDto beatSheet)
    {
        await Get(beatSheetId); // check to see if beat sheet exists before tyring to update
        beatSheet.Id = beatSheetId;
        
        return await beatSheetRepository.Update(beatSheet);
    }
    
    public async void Delete(Guid beatSheetId)
    {
        await Get(beatSheetId); // check to see if beat sheet exists before tyring to delete
        await beatSheetRepository.Delete(beatSheetId);
    }
}