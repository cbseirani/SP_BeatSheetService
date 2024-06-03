using Microsoft.ML.Data;

namespace BeatSheetService.Services.Ai;

public class BeatPrediction
{
    [ColumnName("PredictedLabel")]
    public string PredictedDescription { get; set; }
}

public class ActPrediction
{
    [ColumnName("PredictedLabel")]
    public string PredictedDescription { get; set; }
    
    [ColumnName("PredictedDurationLabel")]
    public float PredictedDuration { get; set; }
    
    [ColumnName("PredictedCameraAngleLabel")]
    public string PredictedCameraAngle { get; set; }
}