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
        Log.Information("[JobBoardDemoApi] [JobController] [Options] starting get request");
        var jobDto = await _jr.Get();
        
        Job[] job = jobDto.Select(x=>_mapper.Map<JobDTO,Job>(x)).Take(50).ToArray();
        return job;
    }

    public Task<bool> Post(Job j)
    {
            JobDTO jd = _mapper.Map<Job,JobDTO>(j);
            return _jr.Post(jd);
    }
    public async Task<Job> GetId(int id)
    {
        var jobDto = _jr.GetId(id);
        Job job = _mapper.Map<JobDTO,Job>(await jobDto);
        return job;    
    }
    public async Task<bool> Delete(int id)
    {
        return await _jr.Delete(id);
    }
    public async Task<bool> Put(Job j)
    {
        JobDTO jd = _mapper.Map<Job,JobDTO>(j);
        return await _jr.Put(jd);
    }
}