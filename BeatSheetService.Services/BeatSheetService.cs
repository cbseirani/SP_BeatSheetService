using BeatSheetService.Common;
using BeatSheetService.Repositories;
using Microsoft.Extensions.Logging;

namespace BeatSheetService.Services;

public interface IBeatSheetService
{
    Task<IEnumerable<BeatSheetDto>> List();
    Task<BeatSheetDto> Get(Guid beatSheetId);
    Task<BeatSheetDto> Create(BeatSheetDto beatSheet);
    Task<BeatSheetDto> Update(Guid beatSheetId, BeatSheetDto beatSheet);
    Task Delete(Guid beatSheetId);
}

public class BeatSheetService(IBeatSheetRepository beatSheetRepository, ILogger<BeatSheetService> logger) : IBeatSheetService
{
    public Task<IEnumerable<BeatSheetDto>> List() => beatSheetRepository.List();
    
    public async Task<BeatSheetDto> Get(Guid beatSheetId)
    {
        // validate
        logger.LogInformation($"Getting beat sheet {beatSheetId}");
        var beatSheet = await beatSheetRepository.Get(beatSheetId);
        if (beatSheet is null)
            throw new NotFoundException($"Beat sheet {beatSheetId} not found!");
        
        return beatSheet;
    }

    public async Task<BeatSheetDto> Create(BeatSheetDto beatSheet)
    {
        logger.LogInformation("Creating beat sheet");
        beatSheet = await beatSheetRepository.Create(beatSheet);
        beatSheet.Beats = new List<BeatDto>();
        logger.LogInformation($"Beat sheet {beatSheet.Id} created");
        return beatSheet;
    }
        
    
    public async Task<BeatSheetDto> Update(Guid beatSheetId, BeatSheetDto beatSheet)
    {
        await Get(beatSheetId); // validate
        
        logger.LogInformation($"Updating beat sheet {beatSheetId}");
        beatSheet.Id = beatSheetId;
        beatSheet = await beatSheetRepository.Update(beatSheet);
        logger.LogInformation($"Updated beat sheet {beatSheetId}");
        return beatSheet;
    }
    
    public async Task Delete(Guid beatSheetId)
    {
        await Get(beatSheetId); // validate
        
        logger.LogInformation($"Deleting beat sheet {beatSheetId}");
        await beatSheetRepository.Delete(beatSheetId);
        logger.LogInformation($"Deleted beat sheet {beatSheetId}");
    }
}