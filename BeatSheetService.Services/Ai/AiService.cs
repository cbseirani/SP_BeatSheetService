using BeatSheetService.Common;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace BeatSheetService.Services.Ai;

public interface IAiService
{
    Task<BeatDto?> SuggestNextBeat(List<BeatDto> beats, BeatDto currentBeat);
    Task<ActDto?> SuggestNextAct(List<ActDto> acts, ActDto currentAct);
}

public class AiService : IAiService 
{
    private PredictionEngine<BeatDto, BeatPrediction>? _beatPredictionEngine;
    private PredictionEngine<ActDto, ActPrediction>? _actPredictionEngine;
    
    public async Task<BeatDto?> SuggestNextBeat(List<BeatDto> beats, BeatDto currentBeat)
    {
        await TrainBeats(beats);
        var suggestion = _beatPredictionEngine?.Predict(currentBeat);
        return new BeatDto { Description = suggestion.PredictedDescription };
    }

    public async Task<ActDto?> SuggestNextAct(List<ActDto> acts, ActDto currentAct)
    {
        await TrainActs(acts);
        var suggestion = _actPredictionEngine?.Predict(currentAct);
        return new ActDto
        {
            Description = suggestion.PredictedDescription,
            Duration = suggestion.PredictedDuration,
            CameraAngle = suggestion.PredictedCameraAngle
        };
    }

    private async Task TrainBeats(IEnumerable<BeatDto> beats)
    {
        var mlContext = new MLContext();
        var data = mlContext.Data.LoadFromEnumerable(beats, SchemaDefinition.Create(typeof(BeatDto)));
        
        //generated using copilot
        var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(BeatDto.Description))
            .Append(mlContext.Transforms.Text.FeaturizeText("DescriptionFeaturized", nameof(BeatDto.Description)))
            .Append(mlContext.Transforms.Concatenate("Features", "DescriptionFeaturized"))
            .Append(mlContext.Transforms.NormalizeMinMax("Features"))
            .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy())
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
            
        var trainedModel = pipeline.Fit(data);
        _beatPredictionEngine = mlContext.Model.CreatePredictionEngine<BeatDto, BeatPrediction>(trainedModel);
    }
    
    private async Task TrainActs(IEnumerable<ActDto> acts)
    {
        var mlContext = new MLContext();
        var data = mlContext.Data.LoadFromEnumerable(acts, SchemaDefinition.Create(typeof(ActDto)));
        
        //generated using copilot
        var pipeline =
            mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(ActDto.Description))
                .Append(mlContext.Transforms.Text.FeaturizeText("DescriptionFeaturized", nameof(ActDto.Description)))
                .Append(mlContext.Transforms.Concatenate("Features", "DescriptionFeaturized"))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy())
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"))
                
                .Append(mlContext.Regression.Trainers.FastTree(labelColumnName: "Duration", featureColumnName: "Features"));

                // TODO: fix camera angle suggestion
                // .Append(mlContext.Transforms.Conversion.MapValueToKey("CameraAngleLabel", nameof(ActDto.CameraAngle)))
                // .Append(mlContext.Transforms.Text.FeaturizeText("CameraAngleFeaturized", nameof(ActDto.CameraAngle)))
                // .Append(mlContext.Transforms.Concatenate("Features", "CameraAngleFeaturized"))
                // .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                // .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy())
                // .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedCameraAngleLabel"));
        
        var trainedModel = pipeline.Fit(data);
        _actPredictionEngine = mlContext.Model.CreatePredictionEngine<ActDto, ActPrediction>(trainedModel);
    }
}