using BeatSheetService.Common;

namespace BeatSheetService.Services;

public interface IActService
{
    Task<ActDto> Create(Guid beatSheetId, Guid beatId, ActDto act);
    Task<ActDto> Update(Guid beatSheetId, Guid beatId, Guid actId, ActDto act);
    void Delete(Guid beatSheetId, Guid beatId, Guid actId);
}


public class ActService(IBeatService beatService) : IActService
{
    public async Task<ActDto> Create(Guid beatSheetId, Guid beatId, ActDto act)
    {
        // check if beatsheet exists
        // check if beat exists
        // add act to beat
        // suggest next act
        // return act
        throw new NotImplementedException();
    }
    
    public async Task<ActDto> Update(Guid beatSheetId, Guid beatId, Guid actId, ActDto act)
    {
        // check if beatsheet exists
        // check if beat exists
        // add act to beat
        // suggest next act
        // return act
        throw new NotImplementedException();
    }
    
    public async void Delete(Guid beatSheetId, Guid beatId, Guid actId)
    {
        throw new NotImplementedException();
    }
}