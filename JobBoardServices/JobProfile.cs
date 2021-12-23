using AutoMapper;
using JobBoardRepository.Domain;
using JobBoardServices.View;

namespace JobBoardServices;

public class JobProfile :Profile
{
    
    public JobProfile()
    {
        CreateMap<Job, JobDTO>();
    }
}