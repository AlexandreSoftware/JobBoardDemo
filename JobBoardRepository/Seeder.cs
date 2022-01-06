using System.Security.Cryptography;
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
    public async static Task Migrate(string cs)
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

    public async static void MigrateAndSeed(string cs1, string cs2)
    {
        await Migrate(cs1);
        await Seed(cs2);
    }
    public async static Task Seed(string cs)
    {
        JobRepository jr = new JobRepository(new DapperWrapper(cs));
        ApplicantRepository ar = new ApplicantRepository(new DapperWrapper(cs));
        foreach (var item in GenerateJobs())
        {
            await jr.Put(item);
        }

        foreach (var item in GenerateApplicants())
        {
            await ar.Put(item);
        }
    }

    public static JobDTO[] GenerateJobs()
    {
        var fakerjob = new Faker<JobDTO>()
            .RuleFor(property: x => x.ProductId, c => c.IndexFaker)
            .RuleFor(property: x => x.Title, setter: c => c.Company + " " + c.Commerce)
            .RuleFor(property: x => x.Description, setter: c => c.Lorem.Word())
            .RuleFor(property: x => x.SubTitle, setter: c => c.Lorem.Word())
            .RuleFor(property: x => x.MaxPay, setter: c => c.Finance.Random.Int())
            .RuleFor(property: x => x.MinPay, setter: (c, usr) =>
            {
                int val;
                do
                {
                    val = c.Finance.Random.Int();
                } while (val > usr.MaxPay);

                return val;
            });
        return fakerjob.Generate(40).ToArray();
    }

    public static ApplicantDTO[] GenerateApplicants()
    {
        var fakerApplicants = new Faker<ApplicantDTO>()
            .RuleFor<int>(property:x=>x.Id,setter:y=>y.IndexFaker)
            .RuleFor<string>(property:x=>x.Name,setter:y=>y.Name.FullName())
            .RuleFor<double>(property:x=>x.WageExpectation,setter:y=>y.Finance.Random.Double());
        return fakerApplicants.Generate(40).ToArray();
    }
}