using System.Data.Common;
using System.Threading.Tasks.Dataflow;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;

namespace JobBoardRepository;

public class JobRepository:IJobRepository
{
    public readonly DapperWrapper _dw;

    public string _cs;
    // public JobRepository(string? cs)
    // {
    //     this._cs = cs;
    // }
    public JobDTO[] Get()
    {
        // return new JobDTO[1]
        // {
        //     // new JobDTO()
        //     // {
        //     //     ProductId = 1,
        //     //     Description = _cs,
        //     //     Title = "aisjai",
        //     //     MaxPay = 100,
        //     //     MinPay = 10,
        //     //     SubTitle = "FOKFSAKO"
        //     // }
        // };
        return null;
    }
    public bool Post(JobDTO j)
    {
        return true;
    }

    public JobDTO GetId(int id)
    {
        return new JobDTO()
        {
            ProductId = 1,
            Description = _cs,
            Title = "aisjai",
            MaxPay = 100,
            MinPay = 10,
            SubTitle = "FOKFSAKO"
        };
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