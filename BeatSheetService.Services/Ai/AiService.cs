using BeatSheetService.Common;
using BeatSheetService.Repositories;
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
    private PredictionEngine<ActDto, ActDto>? _actPredictionEngine;
    
    public async Task<BeatDto?> SuggestNextBeat(List<BeatDto> beats, BeatDto currentBeat)
    {
        await TrainBeats(beats);
        var predictedDescription = _beatPredictionEngine?.Predict(currentBeat);
        return new BeatDto { Description = predictedDescription?.PredictedDescription };
    }

    public async Task<ActDto?> SuggestNextAct(List<ActDto> acts, ActDto currentAct)
    {
        await TrainActs(acts);
        return _actPredictionEngine?.Predict(currentAct);
    }

    private async Task TrainBeats(IEnumerable<BeatDto> beats)
    {
        var mlContext = new MLContext();
        var data = mlContext.Data.LoadFromEnumerable(beats, SchemaDefinition.Create(typeof(BeatDto)));
        
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
        var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Description")
            .Append(mlContext.Transforms.Concatenate("Features", "FeatureColumnName"))
            .Append(mlContext.Transforms.NormalizeMinMax("Features"));

        var trainedModel = pipeline.Fit(data);
        _actPredictionEngine = mlContext.Model.CreatePredictionEngine<ActDto, ActDto>(trainedModel);
    }
}