using System.Data.Common;
using System.Threading.Tasks.Dataflow;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;

namespace JobBoardRepository;

public class JobRepository:IJobRepository
{
    public DbConnection _db;
    public JobRepository(DbConnection db)
    {
        this._db = db;
    }
    public JobDTO[] Get()
    {
        return null;
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