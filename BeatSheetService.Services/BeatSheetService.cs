using BeatSheetService.Repositories;

namespace BeatSheetService.Services;

public interface IBeatSheetService
{
    public void Create();
    public void Get(Guid id);
    public void Update(Guid id);
    public void Delete(Guid id);
    public void List();
}

public class BeatSheetService(IBeatSheetRepository beatSheetRepository) : IBeatSheetService
{
    private readonly IBeatSheetRepository _beatSheetRepository = beatSheetRepository;
    
    public async void Create()
    {
    }
    
    public async void Get(Guid id)
    {
    }
    
    public async void Update(Guid id)
    {
    }
    
    public async void Delete(Guid id)
    {
    }
    
    public async void List()
    {
    }
}