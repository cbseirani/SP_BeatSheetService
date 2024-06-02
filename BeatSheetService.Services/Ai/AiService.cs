using BeatSheetService.Common;
using BeatSheetService.Repositories;
using Microsoft.ML;

namespace BeatSheetService.Services.Ai;

public interface IAiService
{
    Task<BeatDto> SuggestNextBeat(List<BeatDto> beats, BeatDto currentBeat);
    Task<ActDto> SuggestNextAct(List<ActDto> acts, ActDto currentAct);
}

public class AiService : IAiService 
{
    private PredictionEngine<BeatDto, BeatDto> _beatPredictionEngine;
    private PredictionEngine<ActDto, ActDto> _actPredictionEngine;
    
    public async Task<BeatDto> SuggestNextBeat(List<BeatDto> beats, BeatDto currentBeat)
    {
        await TrainBeats(beats);
        return _beatPredictionEngine.Predict(currentBeat);
    }

    public async Task<ActDto> SuggestNextAct(List<ActDto> acts, ActDto currentAct)
    {
        await TrainActs(acts);
        return _actPredictionEngine.Predict(currentAct);
    }

    private async Task TrainBeats(IEnumerable<BeatDto> beats)
    {
        var (mlContext, trainedModel) = await Train(beats);
        _beatPredictionEngine = mlContext.Model.CreatePredictionEngine<BeatDto, BeatDto>(trainedModel);
    }
    
    private async Task TrainActs(IEnumerable<ActDto> acts)
    {
        var (mlContext, trainedModel) = await Train(acts);
        _actPredictionEngine = mlContext.Model.CreatePredictionEngine<ActDto, ActDto>(trainedModel);
    }
    
    private static async Task<(MLContext, ITransformer)> Train<T>(IEnumerable<T> items) where T : class
    {
        // load data
        var mlContext = new MLContext();
        var data = mlContext.Data.LoadFromEnumerable(items);
        var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
            .Append(mlContext.Transforms.Concatenate("Features", "FeatureColumnName"))
            .Append(mlContext.Transforms.NormalizeMinMax("Features"));

        // train
        return (mlContext, pipeline.Fit(data));
    }
}