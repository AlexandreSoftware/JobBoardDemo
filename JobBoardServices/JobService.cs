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
    public Job[] Get()
    {
        Log.Information("[JobBoardDemoApi] [JobController] [Options] starting get request");
        var jobDto = _jr.Get();
        
        Job[] job = jobDto.Select(x=>_mapper.Map<JobDTO,Job>(x)).Take(50).ToArray();
        return job;
    }

    public bool Post(Job j)
    {
            JobDTO jd = _mapper.Map<Job,JobDTO>(j);
            return _jr.Post(jd);
    }
    public Job GetId(int id)
    {
        var jobDto = _jr.GetId(id);
        Job job = _mapper.Map<JobDTO,Job>(jobDto);
        return job;    
    }
    public bool Delete(int id)
    {
        return _jr.Delete(id);
    }
    public bool Put(Job j)
    {
        JobDTO jd = _mapper.Map<Job,JobDTO>(j);
        return _jr.Put(jd);
    }
}