using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.ML.Data;

namespace BeatSheetService.Common;

public class BeatSheetRequestDto
{
    [ValidateNever]
    public string Title { get; set; } // Title of the beat sheet
}

public class BeatSheetDto
{
    public string Id { get; set; } // Unique identifier
    
    public string Title { get; set; } // Title of the beat sheet
    
    public List<BeatDto> Beats { get; set; } = new(); // List of beats
}

public class BeatRequestDto
{
    [ValidateNever]
    public string Description { get; set; } // Description of the beat
}

public class BeatResponseDto
{
    public BeatDto? Beat { get; set; }
    
    public BeatDto? SuggestedNextBeat { get; set; } // AI suggested next Beat
}

public class BeatDto
{
    [NoColumn]
    public string Id { get; set; } // Unique identifier
    
    [LoadColumn(0)]
    public string Description { get; set; } // Description of the beat
    
    [NoColumn]
    public DateTimeOffset? Timestamp { get; set; } // Timestamp of when the beat was created or modified
    
    [NoColumn]
    public List<ActDto> Acts { get; set; } = new(); // List of acts associated with the beat
}

public class ActRequestDto
{
    [ValidateNever]
    public string Description { get; set; } // Description of the act
    
    [ValidateNever]
    public float Duration { get; set; } // Duration of the act in seconds
    
    [ValidateNever]
    public string CameraAngle { get; set; } // Description of the camera angle for the act
}

public class ActResponseDto
{
    public ActDto? Act { get; set; }
    
    public ActDto? SuggestedNextAct { get; set; } // AI suggested next Act
}

public class ActDto
{
    [NoColumn]
    public string Id { get; set; } // Unique identifier
    
    [LoadColumn(0)]
    public string Description { get; set; } // Description of the act
    
    [NoColumn]
    public DateTimeOffset? Timestamp { get; set; } // Timestamp of when the act was created or modified
    
    [LoadColumn(1)]
    public float Duration { get; set; } // Duration of the act in seconds
    
    [LoadColumn(2)]
    public string CameraAngle { get; set; } // Description of the camera angle for the act
}