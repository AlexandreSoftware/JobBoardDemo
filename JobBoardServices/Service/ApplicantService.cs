using AutoMapper;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Serilog;

namespace JobBoardServices.Service;

public class ApplicantService : IApplicantService
{
    private readonly IApplicantRepository _ar;
    private readonly IMapper _mapper;
    private readonly IJobApplicantRepository _jar;
    public ApplicantService(IApplicantRepository ar,IMapper mapper, IJobApplicantRepository jar)
    {
        _ar = ar;
        _mapper = mapper;
        _jar = jar;
    }
    public async Task<Applicant[]> Get()
    {
        string templatelog = "[JobBoardDemoApi] [ApplicantService] [Get] ";
        Log.Information($"{templatelog} starting get request");
        var ApplicantDto = await _ar.Get();
        Log.Information($"{templatelog} got ApplicationDTO[], converting to Application[]");
        var task= ApplicantDto.Select(async x=>
        {
            var applicant= _mapper.Map<ApplicantDTO, Applicant>(x);
            var job = await _jar.GetAppliedJobs(applicant.Id);
            applicant.Jobs =  job.Select(y=> _mapper.Map<JobDTO,Job>(y)).ToArray();
            return applicant;
        });
        var result = task.Select(t=>t.Result).Take(50).ToArray();
        Log.Information($"{templatelog} sucessfully got Application[] returning");
        return result;
    }
    public async Task<Applicant> GetId(int id)
    {
        string templatelog = "[JobBoardDemoApi] [ApplicantService] [GetId] ";
        Log.Information($"{templatelog} Starting GET request");
        var ApplicantDto = _ar.GetId(id);
        Log.Information($"{templatelog} got ApplicationDTO by id,Getting Jobs");
        var jobs = await _jar.GetAppliedJobs(ApplicantDto.Id);
        Log.Information($"{templatelog} Got jobs, converting to Application");
        Applicant Applicant = _mapper.Map<ApplicantDTO,Applicant>(await ApplicantDto);
        Log.Information($"{templatelog} Sucessfully converted Applicant, converting Job");
        Applicant.Jobs = jobs.Select(x=>_mapper.Map<JobDTO,Job>(x)).ToArray();
        Log.Information($"{templatelog} Sucessfully got Application returning");
        return Applicant;    
    }

    public async Task<bool> Delete(int id)
    {
        string templatelog = "[JobBoardDemoApi] [ApplicantService] [Delete] ";
        Log.Information($"{templatelog} Starting DELETE request");
        var res = await _ar.Delete(id);
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
        var res =await _ar.Post(jd);
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
        var res = await _ar.Put(jd);
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