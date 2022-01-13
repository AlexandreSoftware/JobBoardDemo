using System.Collections;
using AutoMapper;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Serilog;

namespace JobBoardServices;

public class JobApplicantService:IJobApplicantService
{
    public readonly IJobApplicantRepository _jar;
    public readonly IMapper _mapper;
    public JobApplicantService(IJobApplicantRepository jar, IMapper mapper)
    {
        _jar = jar;
        _mapper = mapper;
    }
    public async Task<Job[]> GetAppliedJobs(int aid)
    {
        string templatelog = "[JobBoardDemoApi] [JobApplicantService] [GetApliedJobs] ";
        Log.Information($"{templatelog} starting get request");
        var jobDto = await _jar.GetAppliedJobs(aid);
        Log.Information($"{templatelog} got JobDTO[], converting to Job[]");
        Job[] job = jobDto.Select(x=>_mapper.Map<JobDTO,Job>(x)).Take(50).ToArray();
        Log.Information($"{templatelog} sucessfully got Job[] returning");
        return job;
    }

    public async Task<bool> InsertJobApplicant(int jid, int aid)
    {
        string templatelog = "[JobBoardDemoApi] [JobApplicantService] [InsertJobApplicant] ";
        Log.Information($"{templatelog} starting Put request");
        var res =await _jar.InsertJobApplicant(jid, aid);
        Log.Information($"{templatelog} sucessfully inserted JobApplicant returning");
        if (res)
        {
            Log.Information($"{templatelog} Sucessfully inserted, returning");
            return res;
        }
        Log.Information($"{templatelog} Failed to insert, returning");
        return res;
    }

    public async Task<bool> DeleteJobApplicant(int jaid)
    {
        var templatelog = "[JobBoardDemoApi] [JobApplicantService] [DeleteJobApplicant] ";
        Log.Information($"{templatelog} starting Delete request");
        var res = await _jar.DeleteJobApplicant(jaid);
        Log.Information($"{templatelog} sucessfully deleted JobApplicant returning");
        if (res)
        {
            Log.Information($"{templatelog} Sucessfully deleted, returning");
            return res;
        }
        Log.Information($"{templatelog} Failed to delete, returning");
        return res;
    }
}