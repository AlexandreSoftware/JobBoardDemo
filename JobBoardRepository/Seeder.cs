using Bogus;
using JobBoardRepository.Domain;
using Newtonsoft.Json.Linq;
using Serilog;

namespace JobBoardRepository;

public class Seeder
{
    public static readonly DapperWrapper dw;

    public async static void PublishMigrate(string cs)
    {
        string templatelog = "[JobBoardDemoRepository] [Seeder] [PublishMigrate]"; 
        Log.Information($"{templatelog} Starting Migration For Publish");
        Log.Information($"{templatelog} Initializing DapperWrapper ");
        var db = new DapperWrapper(cs);
        Log.Information($"{templatelog} Reading SQL file");
        var sql = await SqlReader.ReadFile("PublishMigrate");
        Log.Information($"{templatelog} Migrating Database");
        db.Execute(sql);
    }
    public async static void Migrate(string cs)
    {
        string templatelog = "[JobBoardDemoRepository] [Seeder] [PublishMigrate]"; 
        Log.Information($"{templatelog} Starting Migration For Development");
        Log.Information($"{templatelog} Initializing DapperWrapper ");
        var db = new DapperWrapper(cs);
        Log.Information($"{templatelog} Reading SQL file");
        var sql = await SqlReader.ReadFile("Migrate");
        Log.Information($"{templatelog} Migrating Database");
        db.Execute(sql);
    }

    public async static void Seed(string cs)
    {
    }
}