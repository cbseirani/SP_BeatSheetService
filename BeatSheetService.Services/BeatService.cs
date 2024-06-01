using BeatSheetService.Repositories;

namespace BeatSheetService.Services;

public interface IBeatService
{
    public void Create();
    void Update(Guid id);
    void Delete(Guid id);
}


public class BeatService(IBeatSheetRepository beatSheetRepository) : IBeatService
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