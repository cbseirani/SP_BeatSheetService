// generated with copilot

namespace BeatSheetService.Repositories;

public class BeatSheet
{
    public Guid Id { get; set; } // Unique identifier
    public string Title { get; set; } // Title of the beat sheet
    public List<Beat> Beats { get; set; } // List of beats
}

public class Beat
{
    public Guid Id { get; set; } // Unique identifier
    public string Description { get; set; } // Description of the beat
    public DateTimeOffset Timestamp { get; set; } // Timestamp of when the beat was created or modified
    public List<Act> Acts { get; set; } // List of acts associated with the beat
}

public class Act
{
    public Guid Id { get; set; } // Unique identifier
    public string Description { get; set; } // Description of the act
    public DateTimeOffset Timestamp { get; set; } // Timestamp of when the act was created or modified
    public float Duration { get; set; } // Duration of the act in seconds
    public string CameraAngle { get; set; } // Description of the camera angle for the act
}