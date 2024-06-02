
// generated with copilot

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BeatSheetService.Common;

public class BeatSheetDto
{
    [ValidateNever]
    public Guid Id { get; set; } // Unique identifier
    [ValidateNever]
    public string Title { get; set; } // Title of the beat sheet
    [ValidateNever]
    public List<BeatDto> Beats { get; set; } = new(); // List of beats
}

public class BeatDto
{
    [ValidateNever]
    public Guid Id { get; set; } // Unique identifier
    [ValidateNever]
    public string Description { get; set; } // Description of the beat
    [ValidateNever]
    public DateTimeOffset? Timestamp { get; set; } // Timestamp of when the beat was created or modified
    [ValidateNever]
    public List<ActDto> Acts { get; set; } = new(); // List of acts associated with the beat
    [ValidateNever]
    public BeatDto SuggestedNextBeat { get; set; } // AI suggested next Beat
}

public class ActDto
{
    [ValidateNever]
    public Guid Id { get; set; } // Unique identifier
    [ValidateNever]
    public string Description { get; set; } // Description of the act
    [ValidateNever]
    public DateTimeOffset? Timestamp { get; set; } // Timestamp of when the act was created or modified
    [ValidateNever]
    public int Duration { get; set; } // Duration of the act in seconds
    [ValidateNever]
    public string CameraAngle { get; set; } // Description of the camera angle for the act
    [ValidateNever]
    public ActDto SuggestedNextAct { get; set; } // AI suggested next Act
}