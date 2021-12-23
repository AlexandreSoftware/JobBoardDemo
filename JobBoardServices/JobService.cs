using System.Data.SqlTypes;
using AutoMapper;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using JobBoardServices.Interface;
using JobBoardServices.View;

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
        return null;
    }

    public bool Post(Job j)
    {
            JobDTO jd = _mapper.Map<Job,JobDTO>(j);
            return _jr.Post(jd);
    }
    public Job GetId(int id)
    {
        return null;
    }
    public bool Delete(int id)
    {
        return true;
    }
    public bool Put(Job j)
    {
        return true;
    }
    public void Options()
    {
    }
}