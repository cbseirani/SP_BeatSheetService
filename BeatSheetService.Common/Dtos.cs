
// generated with copilot

namespace BeatSheetService.Common;

public class BeatSheetDto
{
    public Guid Id { get; set; } // Unique identifier
    public string Title { get; set; } // Title of the beat sheet
    public List<BeatDto> Beats { get; set; } // List of beats
}

public class BeatDto
{
    public Guid Id { get; set; } // Unique identifier
    public string Description { get; set; } // Description of the beat
    public DateTime Timestamp { get; set; } // Timestamp of when the beat was created or modified
    public List<ActDto> Acts { get; set; } // List of acts associated with the beat
    public BeatDto SuggestedNextBeat { get; set; } // AI suggested next Beat
}

public class ActDto
{
    public Guid Id { get; set; } // Unique identifier
    public string Description { get; set; } // Description of the act
    public DateTime Timestamp { get; set; } // Timestamp of when the act was created or modified
    public int Duration { get; set; } // Duration of the act in seconds
    public string CameraAngle { get; set; } // Description of the camera angle for the act
    public ActDto SuggestedNextAct { get; set; } // AI suggested next Act
}