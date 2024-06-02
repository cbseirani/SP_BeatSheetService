using Microsoft.ML.Data;

namespace BeatSheetService.Services.Ai;

public class BeatPrediction
{
    [ColumnName("PredictedLabel")]
    public string PredictedDescription { get; set; }
}