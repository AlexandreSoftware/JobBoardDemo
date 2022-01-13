using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace JobBoardRepository;

public class JobApplicantRepository : IJobApplicantRepository
{
    private readonly IDapperWrapper _dw;
    public JobApplicantRepository(IDapperWrapper _dw)
    {
        this._dw = _dw;
    }
    public async Task<JobDTO[]> GetAppliedJobs(int aid)
    {
        string templatelog = "[JobBoardDemoRepository] [JobApplicantRepository] [GetAppliedJobs]";
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("JobApplicant/GetAppliedJobs");
        Log.Information($"{templatelog} Got Sql String, Querying");
        var x = _dw.QueryAsyncParams<JobDTO>(sql, new {Id = aid}).Result;
        Log.Information($"{templatelog} Query done, returning");
        return x.ToArray();
    }
    public async Task<bool> InsertJobApplicant(int jid, int aid)
    {
        string templatelog = "[JobBoardDemoRepository] [JobApplicantRepository] [InsertJobApplicant]";
        JobApplicantDTO jadto = new JobApplicantDTO{JobId = jid, ApplicantId = aid,Status = "NotViewed"};;
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("JobApplicant/InsertJobApplicant");
        Log.Information($"{templatelog} Got Sql String, Executing");
        _dw.ExecuteParamsAsync(sql, jadto);
        return true;
    }
    public async Task<bool> DeleteJobApplicant(int jaid)
    {
        string templatelog = "[JobBoardDemoRepository] [JobApplicantRepository] [DeleteJobApplicant]";
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("JobApplicant/DeleteJobApplicant");
        Log.Information($"{templatelog} Got Sql String, Executing");
        _dw.ExecuteParamsAsync(sql, new{Id = jaid});;
        return true;
    }
}
