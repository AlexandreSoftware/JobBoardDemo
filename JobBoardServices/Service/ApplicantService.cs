using AutoMapper;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using JobBoardServices.Interface;
using Serilog;

namespace JobBoardServices.Service;

public class ApplicantService : IApplicantService
{
    private readonly IApplicantRepository _jr;
    private readonly IMapper _mapper;

    public ApplicantService(IApplicantRepository jr,IMapper mapper)
    {
        
        this._jr = jr;
        _mapper = mapper;
    }
    public async Task<Applicant[]> Get()
    {
        string templatelog = "[JobBoardDemoApi] [ApplicantService] [Get] ";
        Log.Information($"{templatelog} starting get request");
        var jobDto = await _jr.Get();
        Log.Information($"{templatelog} got ApplicationDTO[], converting to Application[]");
        Applicant[] job = jobDto.Select(x=>_mapper.Map<ApplicantDTO,Applicant>(x)).Take(50).ToArray();
        Log.Information($"{templatelog} sucessfully got Application[] returning");
        return job;
    }
    public async Task<Applicant> GetId(int id)
    {
        string templatelog = "[JobBoardDemoApi] [ApplicantService] [GetId] ";
        Log.Information($"{templatelog} Starting GET request");
        var jobDto = _jr.GetId(id);
        Log.Information($"{templatelog} Got ApplicationDTO by id, converting to Application");
        Applicant job = _mapper.Map<ApplicantDTO,Applicant>(await jobDto);
        Log.Information($"{templatelog} Sucessfully got Application returning");
        return job;    
    }

    public async Task<bool> Delete(int id)
    {
        string templatelog = "[JobBoardDemoApi] [ApplicantService] [Delete] ";
        Log.Information($"{templatelog} Starting DELETE request");
        var res = await _jr.Delete(id);
        Log.Information($"{templatelog} Called Delete");
        if (res)
        {
            Log.Information($"{templatelog} Sucessfully deleted, returning");
            return res;
        }
        Log.Information($"{templatelog} Failed to delete, returning");
        return res;
    }

    public async Task<bool> Post(Applicant j)
    {
        string templatelog = "[JobBoardDemoApi] [ApplicantService] [POST] ";
        Log.Information($"{templatelog} starting POST request mapping");
        ApplicantDTO jd = _mapper.Map<Applicant,ApplicantDTO>(j);
        Log.Information($"{templatelog} mapped object calling Repository");
        var res =await _jr.Post(jd);
        Log.Information($"{templatelog} Sucessfully updated, returning");
        if (res)
        {
            Log.Information($"{templatelog} Sucessfull POST, returning");
            return res;
        }
        Log.Information($"{templatelog} Failed to POST, returning");
        return res;

    }
    public async Task<bool> Put(Applicant j)
    {
        string templatelog = "[JobBoardDemoApi] [ApplicantService] [PUT] ";
        Log.Information($"{templatelog} starting PUT request mapping");
        ApplicantDTO jd = _mapper.Map<Applicant,ApplicantDTO>(j);
        Log.Information($"{templatelog} mapped object calling Repository");
        var res = await _jr.Put(jd);
        Log.Information($"{templatelog} Called PUT");
        if (res)
        {
            Log.Information($"{templatelog} Sucessfully PUT, returning");
            return res;
        }
        Log.Information($"{templatelog} Failed to PUT, returning");
        return res;
    }
}