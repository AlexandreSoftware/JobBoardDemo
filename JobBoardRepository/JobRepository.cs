using System.Data.Common;
using System.Threading.Tasks.Dataflow;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;

namespace JobBoardRepository;

public class JobRepository:IJobRepository
{
    public readonly DapperWrapper _dw;

    
    public JobDTO[] Get()
    {
        return new JobDTO[]
        {
            new JobDTO()
            {
                ProductId = 1
            }
        };
}
    public bool Post(JobDTO j)
    {
        return true;
    }

    public JobDTO GetId(int id)
    {
        return null;
    }
    public bool Delete(int id)
    {
        return true;
    }
    public bool Put(JobDTO j)
    {
        return true;
    }
    public void Options()
    {
    }
}