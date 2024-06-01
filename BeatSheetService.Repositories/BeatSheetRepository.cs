using BeatSheetService.Common;
using Mapster;
using MongoDB.Driver;

namespace BeatSheetService.Repositories;

public interface IBeatSheetRepository
{
}

public class BeatSheetRepository(IMongoDatabase database) : IBeatSheetRepository
{
    private readonly IMongoCollection<BeatSheet> _beatSheetCollection = database.GetCollection<BeatSheet>("beatSheets");

    public async Task CreateBeatSheet(BeatSheetDto beatSheet)
    {
        await _beatSheetCollection.InsertOneAsync(beatSheet.Adapt<BeatSheet>());
    }

    public async Task<BeatSheet> GetBeatSheet(Guid id)
    {
        var filter = Builders<BeatSheet>.Filter.Eq(bs => bs.Id, id);
        return await _beatSheetCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateBeatSheet(Guid id, BeatSheet updatedBeatSheet)
    {
        var filter = Builders<BeatSheet>.Filter.Eq(bs => bs.Id, id);
        await _beatSheetCollection.ReplaceOneAsync(filter, updatedBeatSheet);
    }

    public async Task DeleteBeatSheet(Guid id)
    {
        var filter = Builders<BeatSheet>.Filter.Eq(bs => bs.Id, id);
        await _beatSheetCollection.DeleteOneAsync(filter);
    }

    public async Task<List<BeatSheet>> ListBeatSheets()
    {
        return await _beatSheetCollection.Find(_ => true).ToListAsync();
    }
}