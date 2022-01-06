using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using Serilog;

namespace JobBoardRepository;

public class ApplicantRepository : IApplicantRepository
{
    public IDapperWrapper _dw;

    public ApplicantRepository(IDapperWrapper dw){

        this._dw = dw;
    }

    public async Task<ApplicantDTO[]> Get()
    {
        string templatelog = "[JobBoardDemoRepository] [ApplicantRepository] [Get]";
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("Applicant/Get");
        Log.Information($"{templatelog} Got Sql String, Querying");
        var x = _dw.QueryAsync<ApplicantDTO>(sql).Result;
        Log.Information($"{templatelog} Query done, returning");
        return x.ToArray();
    }
    public async Task<bool> Post(ApplicantDTO ap)
    {
        string templatelog = "[JobBoardDemoRepository] [ApplicantRepository] [Post]";
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("Applicant/Post");
        Log.Information($"{templatelog} Got Sql String, Querying");
        _dw.ExecuteParams<ApplicantDTO>(@sql,ap);
        Log.Information($"{templatelog} Query done, returning");
        return true;
    }

    public async Task<ApplicantDTO> GetId(int id)
    {
        string templatelog = "[JobBoardDemoRepository] [ApplicantRepository] [Post]";
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("Applicant/GetId");
        Log.Information($"{templatelog} Got Sql String, Querying");
        var x = await _dw.QueryAsyncParams<ApplicantDTO>(@sql,new{Id=id});
        Log.Information($"{templatelog} Query done, returning");
        return x.ToList()[0];

    }
    public async  Task<bool> Delete(int id)
    {
        string templatelog = "[JobBoardDemoRepository] [ApplicantRepository] [Post]";
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("Applicant/Delete");
        Log.Information($"{templatelog} Got Sql String, Querying");
        _dw.ExecuteParams(@sql,new{Id=id});
        Log.Information($"{templatelog} Query done, returning");
        return true;
    }
    public  async Task<bool> Put(ApplicantDTO ap)
    {
        string templatelog = "[JobBoardDemoRepository] [ApplicantRepository] [Post]";
        Log.Information($"{templatelog} Starting Repository, Finding File");
        var sql = await SqlReader.ReadFile("Applicant/Put");
        Log.Information($"{templatelog} Got Sql String, Querying");
        _dw.ExecuteParams<ApplicantDTO>(@sql,ap);
        Log.Information($"{templatelog} Query done, returning");
        return true;
    }
}