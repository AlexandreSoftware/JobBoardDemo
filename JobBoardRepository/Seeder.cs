﻿using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Bogus;
using JobBoardRepository.Domain;
using Newtonsoft.Json.Linq;
using Serilog;

namespace JobBoardRepository;

public class Seeder
{
    public static readonly DapperWrapper dw;
    public static readonly SqlReader sqlReader;
    /// <summary>
    /// Migration Method to migrate the database for production
    /// </summary>
    /// <param name="cs">Connection string</param>
    public async static void PublishMigrate(string cs)
    {
        string templatelog = "[JobBoardDemoRepository] [Seeder] [PublishMigrate]"; 
        Log.Information($"{templatelog} Starting Migration For Publish");
        Log.Information($"{templatelog} Initializing DapperWrapper ");
        var db = new DapperWrapper(cs);
        Log.Information($"{templatelog} Reading SQL file");
        var sql = await sqlReader.ReadFile("PublishMigrate");
        Log.Information($"{templatelog} Migrating Database");
        db.Execute(sql);
    }
    /// <summary>
    /// Migration Method to migrate the database for development, this will drop the database and recreate it
    /// </summary>
    /// <param name="cs">Connection string</param>
    public async static Task Migrate(string cs)
    {
        string templatelog = "[JobBoardDemoRepository] [Seeder] [PublishMigrate]"; 
        Log.Information($"{templatelog} Starting Migration For Development");
        Log.Information($"{templatelog} Initializing DapperWrapper ");
        var db = new DapperWrapper(cs);
        Log.Information($"{templatelog} Reading SQL file");
        var sql = await sqlReader.ReadFile("Migrate");
        Log.Information($"{templatelog} Migrating Database");
        db.Execute(sql);
    }
    
    public async static void MigrateAndSeed(string cs1, string cs2)
    {
        string templatelog = "[JobBoardDemoRepository] [Seeder] [MigrateAndSeed]";
        Log.Information($"{templatelog} Starting Migration");
        await Migrate(cs1);
        Log.Information($"{templatelog} Migration done");
        Log.Information($"{templatelog} Starting Seeding");
        await Seed(cs2);
    }
    public async static Task Seed(string cs)
    {
        string templatelog = "[JobBoardDemoRepository] [Seeder] [Seed]";
        Log.Information($"{templatelog} Starting seeding, instantiating repository");
        JobRepository jr = new JobRepository(new DapperWrapper(cs),sqlReader);
        ApplicantRepository ar = new ApplicantRepository(new DapperWrapper(cs),sqlReader);
        JobApplicantRepository jar = new JobApplicantRepository(new DapperWrapper(cs),sqlReader);
        Log.Information($"{templatelog} Generating Jobs");
        var jobs = GenerateJobs();
        Log.Information($"{templatelog} Generating Applicants");
        var applicants = GenerateApplicants();
        Log.Information($"{templatelog} Inserting jobs and applicants");
        
        for (int i = 0; i < jobs.Length; i++)
        {
            await jr.Put(jobs[i]);
            await ar.Put(applicants[i]);
        }
        Log.Information($"{templatelog} Done inserting jobs and applicants");
        int[] RandomApplicantsIds = Shuffle40Random();
        int[] RandomJobsIds = Shuffle40Random();
        Log.Information($"{templatelog} Inserting job applicants");
        for(int i=0;i<40;i++)
        {
            await jar.InsertJobApplicant(RandomJobsIds[i],RandomApplicantsIds[i]);
        }
        
    }
    public static int[] Shuffle40Random()
    {
        int[] result = new int[40];
        Random r = new Random();
        for (int i = 0; i < 40; i++)
        {
            result[i] = r.Next(1, 41);
        }
        return result;
    }
    public static JobDTO[] GenerateJobs()
    {
        string templatelog = "[JobBoardDemoRepository] [Seeder] [GenerateJobs]";
        Log.Information($"{templatelog} Creating Job Generator faker");
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
        Log.Information($"{templatelog} Generating Jobs");
        var res = fakerjob.Generate(40).ToArray();
        Log.Information($"{templatelog} Jobs Generated, returning");
        return res;
    }

    public static ApplicantDTO[] GenerateApplicants()
    {
        string templatelog = "[JobBoardDemoRepository] [Seeder] [GenerateJobs]";
        Log.Information($"{templatelog} Creating Applicant Generator faker");
        var fakerApplicants = new Faker<ApplicantDTO>()
            .RuleFor<int>(property:x=>x.Id,setter:y=>y.IndexFaker)
            .RuleFor<string>(property:x=>x.Name,setter:y=>y.Name.FullName())
            .RuleFor<double>(property:x=>x.WageExpectation,setter:y=>y.Finance.Random.Double());
        Log.Information($"{templatelog} Generating Applicants");
        var res=  fakerApplicants.Generate(40).ToArray();
        Log.Information($"{templatelog} Applicants Generated, returning");
        return res;
    }
}