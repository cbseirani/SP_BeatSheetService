using BeatSheetService.Repositories;
using MongoDB.Driver;

namespace BeatSheetService.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // var dbConnString = configuration.GetSection("DUPLOCORE_DBCONNSTRING").Value ?? string.Empty;
        //
        // services.AddSingleton(typeof(IMongoClient), new MongoClient(dbConnString));
        // services.AddSingleton(typeof(IMongoDatabase), x =>
        // {
        //     var client = x.GetRequiredService<IMongoClient>();
        //     return client.GetDatabase("BeatSheetDb");
        // });
        //
        // services.AddSingleton(typeof(IMongoCollection<BeatSheet>), x =>
        // {
        //     var database = x.GetRequiredService<IMongoDatabase>();
        //     return database.GetCollection<BeatSheet>("BeatSheets");
        // });
    }
}