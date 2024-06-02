using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.ML.Data;

namespace BeatSheetService.Common;

public class BeatSheetDto
{
    [ValidateNever]
    public string Id { get; set; } // Unique identifier
    
    [ValidateNever]
    public string Title { get; set; } // Title of the beat sheet
    
    [ValidateNever]
    public List<BeatDto> Beats { get; set; } = new(); // List of beats
}

public class BeatDto
{
    [ValidateNever]
    [NoColumn]
    public string Id { get; set; } // Unique identifier
    
    [ValidateNever]
    [LoadColumn(0)]
    public string Description { get; set; } // Description of the beat
    
    [ValidateNever]
    [NoColumn]
    public DateTimeOffset? Timestamp { get; set; } // Timestamp of when the beat was created or modified
    
    [ValidateNever]
    [NoColumn]
    public List<ActDto> Acts { get; set; } = new(); // List of acts associated with the beat
    
    [ValidateNever]
    [NoColumn]
    public BeatDto? SuggestedNextBeat { get; set; } // AI suggested next Beat
}

public class ActDto
{
    [ValidateNever]
    [NoColumn]
    public string Id { get; set; } // Unique identifier
    
    [ValidateNever]
    [LoadColumn(0)]
    public string Description { get; set; } // Description of the act
    
    [ValidateNever]
    [NoColumn]
    public DateTimeOffset? Timestamp { get; set; } // Timestamp of when the act was created or modified
    
    [ValidateNever]
    [LoadColumn(1)]
    public int Duration { get; set; } // Duration of the act in seconds
    
    [ValidateNever]
    [LoadColumn(2)]
    public string CameraAngle { get; set; } // Description of the camera angle for the act
    
    [ValidateNever]
    [NoColumn]
    public ActDto? SuggestedNextAct { get; set; } // AI suggested next Act
}