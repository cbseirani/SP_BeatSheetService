using BeatSheetService.Common;
using Mapster;
using MongoDB.Driver;

namespace BeatSheetService.Repositories;

public interface IBeatSheetRepository
{
    Task<IEnumerable<BeatSheetDto>> List();
    Task<BeatSheetDto> Get(Guid id);
    Task<BeatSheetDto> Create(BeatSheetDto beatSheet);
    Task<BeatSheetDto> Update(BeatSheetDto beatSheet);
    Task Delete(Guid id);

}

public class BeatSheetRepository(IMongoCollection<BeatSheet> beatSheetCollection) : IBeatSheetRepository
{
    public async Task<IEnumerable<BeatSheetDto>> List()
    {
        var list = await beatSheetCollection.Find(_ => true).ToListAsync();
        return list.Adapt<IEnumerable<BeatSheetDto>>();
    }
    
    public async Task<BeatSheetDto> Get(Guid id)
    {
        var filter = Builders<BeatSheet>.Filter.Eq(bs => bs.Id, id);
        var beatSheet = await beatSheetCollection.Find(filter).FirstOrDefaultAsync();
        return beatSheet.Adapt<BeatSheetDto>();
    }
    
    public async Task<BeatSheetDto> Create(BeatSheetDto beatSheet)
    {
        var newBeatSheet = beatSheet.Adapt<BeatSheet>();
        await beatSheetCollection.InsertOneAsync(newBeatSheet);
        return newBeatSheet.Adapt<BeatSheetDto>();
    }

    public async Task<BeatSheetDto> Update(BeatSheetDto beatSheet)
    {
        var updatedBeatSheet = beatSheet.Adapt<BeatSheet>();
        var filter = Builders<BeatSheet>.Filter.Eq(bs => bs.Id, updatedBeatSheet.Id);
        await beatSheetCollection.ReplaceOneAsync(filter, updatedBeatSheet);
        return updatedBeatSheet.Adapt<BeatSheetDto>();
    }

    public async Task Delete(Guid id)
    {
        var filter = Builders<BeatSheet>.Filter.Eq(bs => bs.Id, id);
        await beatSheetCollection.DeleteOneAsync(filter);
    }
}