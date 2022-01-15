using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using Serilog;

namespace JobBoardRepository;

public class ApplicantRepository : IApplicantRepository
{
    public IDapperWrapper _dw;
    public ISqlReader _sqlReader;

    public ApplicantRepository(IDapperWrapper dw, ISqlReader sqlReader)
    {
        this._dw = dw;
        this._sqlReader = sqlReader;
    }

    public async Task<ApplicantDTO[]> Get()
    {
        string TemplateLog = "[JobBoardDemoRepository] [ApplicantRepository] [Get]";
        Log.Information("{TemplateLog} Starting Repository, Finding File",TemplateLog);
        var sql = await _sqlReader.ReadFile("Applicant/Get");
        Log.Information("{TemplateLog} Got Sql String, Querying",TemplateLog);
        try
        {
            var x = _dw.QueryAsync<ApplicantDTO>(sql).Result;
            Log.Information("{TemplateLog} Query done, returning", TemplateLog);
            return x.ToArray();
        }
        catch (Exception ex)
        {
                    Log.Error("{TemplateLog} [ERROR] Error in Repository, {ex}", TemplateLog, ex);

            return null;
        }
    }
    public async Task<bool> Post(ApplicantDTO ap)
    {
        string TemplateLog = "[JobBoardDemoRepository] [ApplicantRepository] [Post]";
        Log.Information("{TemplateLog} Starting Repository, Finding File",TemplateLog);
        var sql = await _sqlReader.ReadFile("Applicant/Post");
        Log.Information("{TemplateLog} Got Sql String, Querying",TemplateLog);
        try
        {
            _dw.ExecuteParamsAsync<ApplicantDTO>(@sql,ap);
            Log.Information("{TemplateLog} Query done, returning",TemplateLog);
            return true;
        }
        catch (Exception ex)
        {
            Log.Error("{TemplateLog} [ERROR] Error in Repository, {ex}", TemplateLog, ex);
            return false;
        }
    }

    public async Task<ApplicantDTO> GetId(int id)
    {
        string TemplateLog = "[JobBoardDemoRepository] [ApplicantRepository] [Post]";
        Log.Information("{TemplateLog} Starting Repository, Finding File",TemplateLog);
        var sql = await _sqlReader.ReadFile("Applicant/GetId");
        Log.Information("{TemplateLog} Got Sql String, Querying",TemplateLog);
        try
        {
            var x = await _dw.QueryAsyncParams<ApplicantDTO>(@sql, new {Id = id});
            Log.Information("{TemplateLog} Query done, returning", TemplateLog);
            if (x.Count() > 0)
            {
                return x.First();
            }
            else
            {
                throw new Exception();
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
        string TemplateLog = "[JobBoardDemoRepository] [ApplicantRepository] [Post]";
        Log.Information("{TemplateLog} Starting Repository, Finding File",TemplateLog);
        var sql = await _sqlReader.ReadFile("Applicant/Delete");
        Log.Information("{TemplateLog} Got Sql String, Querying",TemplateLog);
        try
        {
            _dw.ExecuteParamsAsync(@sql, new {Id = id});
            Log.Information("{TemplateLog} Query done, returning",TemplateLog);
            return true;
        }
        catch (Exception ex)
        {
            Log.Error("{TemplateLog} [ERROR] Error in Repository, {ex}", TemplateLog, ex);
            return false;
        }
    }
    public  async Task<bool> Put(ApplicantDTO ap)
    {
        string TemplateLog = "[JobBoardDemoRepository] [ApplicantRepository] [Post]";
        Log.Information("{TemplateLog} Starting Repository, Finding File",TemplateLog);
        var sql = await _sqlReader.ReadFile("Applicant/Put");
        Log.Information("{TemplateLog} Got Sql String, Querying",TemplateLog);
        try
        {
            _dw.ExecuteParamsAsync<ApplicantDTO>(@sql, ap);
            Log.Information("{TemplateLog} Query done, returning", TemplateLog);
            return true;
        }
        catch (Exception ex)
        {
            Log.Error("{TemplateLog} [ERROR] Error in Repository, {ex}", TemplateLog, ex);
            return false;
        }

    }
}