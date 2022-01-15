using System.Data.Common;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using Serilog;

namespace JobBoardRepository;

public class JobRepository : IJobRepository
{
    public IDapperWrapper _dw;
    public ISqlReader _sqlReader;
    public JobRepository(IDapperWrapper dw, ISqlReader sqlReader)
    {
        _dw = dw;
        _sqlReader = sqlReader;
    }

    public async Task<JobDTO[]> Get()
    {
        string TemplateLog = "[JobBoardDemoRepository] [JobRepository] [Get]";
        Log.Information("{TemplateLog} Starting Repository, Finding File",TemplateLog);
        var sql = await _sqlReader.ReadFile("Job/Get");
        Log.Information("{TemplateLog} Got Sql String, Querying",TemplateLog);
        try
        {

            var x = _dw.QueryAsync<JobDTO>(sql).Result;
            Log.Information("{TemplateLog} Query done, returning", TemplateLog);
            if(x.Count() == 0)
            {
                Log.Information("{TemplateLog} No Jobs Found, returning empty array", TemplateLog);
                return new JobDTO[0];
            }
            else
            {
                Log.Information("{TemplateLog} Jobs Found, returning", TemplateLog);
                return x.ToArray();
            }
            
        }
        catch (Exception ex)
        {
            Log.Error("{TemplateLog} [ERROR] Error in Repository, {ex}", TemplateLog, ex);
            throw;
        }
    }
    public async Task<bool> Post(JobDTO j)
    {
        string TemplateLog = "[JobBoardDemoRepository] [JobRepository] [Post]";
        Log.Information("{TemplateLog} Starting Repository, Finding File",TemplateLog);
        var sql = await _sqlReader.ReadFile("Job/Post");
        Log.Information("{TemplateLog} Got Sql String, Querying",TemplateLog);
        try{
            _dw.ExecuteParamsAsync<JobDTO>(@sql,j);
            Log.Information("{TemplateLog} Query done, returning",TemplateLog);
            return true;
        }
        catch
        {
            
            return false;
        }
    }

    public async Task<JobDTO> GetId(int id)
    {
        string TemplateLog = "[JobBoardDemoRepository] [JobRepository] [Post]";
        Log.Information("{TemplateLog} Starting Repository, Finding File",TemplateLog);
        var sql = await _sqlReader.ReadFile("Job/GetId");
        Log.Information("{TemplateLog} Got Sql String, Querying",TemplateLog);
        try{
            var x = await _dw.QueryAsyncParams<JobDTO>(@sql,new{ProductId=id});
            Log.Information("{TemplateLog} Query done, returning",TemplateLog);
            if(x.Count() == 0)
            {
                Log.Information("{TemplateLog} No Jobs Found, Throwing error",TemplateLog);
                throw new Exception("No Jobs Found");
            }
            else
            {
                Log.Information("{TemplateLog} Jobs Found, returning",TemplateLog);
                return x.First();
            }
        }
        catch(Exception ex)
        {
            Log.Error("{TemplateLog} [ERROR] Error in Repository, {ex}", TemplateLog, ex);
            throw;
        }

    }
    public async  Task<bool> Delete(int id)
    {
        string TemplateLog = "[JobBoardDemoRepository] [JobRepository] [Post]";
        Log.Information("{TemplateLog} Starting Repository, Finding File",TemplateLog);
        var sql = await _sqlReader.ReadFile("Job/Delete");
        Log.Information("{TemplateLog} Got Sql String, Querying",TemplateLog);
        try{
            _dw.ExecuteParamsAsync(@sql,new{ProductId=id});
            Log.Information("{TemplateLog} Query done, returning",TemplateLog);
            return true;
        }
        catch(Exception e)
        {
            Log.Error("{TemplateLog} [ERROR] ERROR in repository:{e.Message}",TemplateLog); 
            return false;
        }
    }
    public  async Task<bool> Put(JobDTO j)
    {
        string TemplateLog = "[JobBoardDemoRepository] [JobRepository] [Post]";
        Log.Information("{TemplateLog} Starting Repository, Finding File",TemplateLog);
        var sql = await _sqlReader.ReadFile("Job/Put");
        Log.Information("{TemplateLog} Got Sql String, Querying",TemplateLog);
        try{
            _dw.ExecuteParamsAsync<JobDTO>(@sql,j);
            Log.Information("{TemplateLog} Query done, returning",TemplateLog);
            return true;
        }
        catch(Exception e)
        {
            Log.Error("{TemplateLog} [ERROR] ERROR in repository:{e.Message}",TemplateLog); 
            return false;
        }
    }
}