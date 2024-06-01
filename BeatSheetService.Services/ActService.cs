using BeatSheetService.Repositories;

namespace BeatSheetService.Services;

public interface IActService
{
    public void Create();
    void Update(Guid id);
    void Delete(Guid id);
}


public class ActService(IBeatSheetRepository beatSheetRepository) : IActService
{
    private readonly IBeatSheetRepository _beatSheetRepository = beatSheetRepository;
    
    public async void Create()
    {
    }
    
    public async void Update(Guid id)
    {
    }
    
    public async void Delete(Guid id)
    {
    }
}