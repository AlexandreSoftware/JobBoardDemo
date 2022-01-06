using System.Data.Common;
using System.Threading.Tasks.Dataflow;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using Serilog;

namespace JobBoardRepository;

public class JobRepository : IJobRepository
{
    public IDapperWrapper _dw;

    public JobRepository(IDapperWrapper dw){

        this._dw = dw;
    }

    public async Task<JobDTO[]> Get()
    {
        string templatelog = "[JobBoardDemoRepository] [JobRepository] [Get]";
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("Job/Get");
        Log.Information($"{templatelog} Got Sql String, Querying");
        var x = _dw.QueryAsync<JobDTO>(sql).Result;
        Log.Information($"{templatelog} Query done, returning");
        return x.ToArray();
    }
    public async Task<bool> Post(JobDTO j)
    {
        string templatelog = "[JobBoardDemoRepository] [JobRepository] [Post]";
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("Job/Post");
        Log.Information($"{templatelog} Got Sql String, Querying");
        _dw.ExecuteParamsAsync<JobDTO>(@sql,j);
        Log.Information($"{templatelog} Query done, returning");
        return true;
    }

    public async Task<JobDTO> GetId(int id)
    {
        string templatelog = "[JobBoardDemoRepository] [JobRepository] [Post]";
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("Job/GetId");
        Log.Information($"{templatelog} Got Sql String, Querying");
        var x = await _dw.QueryAsyncParams<JobDTO>(@sql,new{ProductId=id});
        Log.Information($"{templatelog} Query done, returning");
        return x.ToList()[0];

    }
    public async  Task<bool> Delete(int id)
    {
        string templatelog = "[JobBoardDemoRepository] [JobRepository] [Post]";
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("Job/Delete");
        Log.Information($"{templatelog} Got Sql String, Querying");
        _dw.ExecuteParamsAsync(@sql,new{ProductId=id});
        Log.Information($"{templatelog} Query done, returning");
        return true;
    }
    public  async Task<bool> Put(JobDTO j)
    {
        string templatelog = "[JobBoardDemoRepository] [JobRepository] [Post]";
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("Job/Put");
        Log.Information($"{templatelog} Got Sql String, Querying");
        _dw.ExecuteParamsAsync<JobDTO>(@sql,j);
        Log.Information($"{templatelog} Query done, returning");
        return true;
    }
}