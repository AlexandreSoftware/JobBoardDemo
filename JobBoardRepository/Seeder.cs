using Serilog;

namespace JobBoardRepository;

public class Seeder
{
    public static readonly DapperWrapper dw;

    public async static void Migrate(string cs)
    {
        Log.Information("[JobBoardDemoRepository] [Seeder] [Migrate] Starting Migration");
        Console.WriteLine(cs);
        var db = new DapperWrapper(cs);
        var sql = await SqlReader.ReadFile("Migrate");
        db.QueryNoReturnAsync(sql);
    }

    public static void Seed(string cs)
    {
        
    }
}