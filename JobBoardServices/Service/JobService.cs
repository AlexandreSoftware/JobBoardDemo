using System.Data.SqlTypes;
using AutoMapper;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Serilog;

namespace JobBoardServices;

public class JobService:IJobService
{
    private IJobRepository _jr;
    private IMapper _mapper;

    public JobService(IJobRepository jr,IMapper mapper)
    {
        
        this._jr = jr;
        _mapper = mapper;
    }
    public async Task<Job[]> Get()
    {
        string templatelog = "[JobBoardDemoApi] [JobService] [Get] ";
        Log.Information($"{templatelog} starting get request");
        var jobDto = await _jr.Get();
        Log.Information($"{templatelog} got ApplicationDTO[], converting to Application[]");
        Job[] job = jobDto.Select(x=>_mapper.Map<JobDTO,Job>(x)).Take(50).ToArray();
        Log.Information($"{templatelog} sucessfully got Application[] returning");
        return job;
    }
    public async Task<Job> GetId(int id)
    {
        string templatelog = "[JobBoardDemoApi] [JobService] [GetId] ";
        Log.Information($"{templatelog} Starting GET request");
        var jobDto = _jr.GetId(id);
        Log.Information($"{templatelog} Got ApplicationDTO by id, converting to Application");
        Job job = _mapper.Map<JobDTO,Job>(await jobDto);
        Log.Information($"{templatelog} Sucessfully got Application returning");
        return job;    
    }

    public async Task<bool> Delete(int id)
    {
        string templatelog = "[JobBoardDemoApi] [JobService] [Delete] ";
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

    public async Task<bool> Post(Job j)
    {
        string templatelog = "[JobBoardDemoApi] [JobService] [POST] ";
        Log.Information($"{templatelog} starting POST request mapping");
        JobDTO jd = _mapper.Map<Job,JobDTO>(j);
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
    public async Task<bool> Put(Job j)
    {
        string templatelog = "[JobBoardDemoApi] [JobService] [PUT] ";
        Log.Information($"{templatelog} starting PUT request mapping");
        JobDTO jd = _mapper.Map<Job,JobDTO>(j);
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