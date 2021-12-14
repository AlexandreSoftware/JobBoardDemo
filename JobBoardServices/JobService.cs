using System.Data.SqlTypes;
using JobBoardServices.Interface;
using JobBoardServices.View;

namespace JobBoardServices;

public class JobService:IJobService
{
    public Job[] Get()
    {
        return null;
    }
    public bool Post(Job j)
    {
        return false;
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
        return false;
    }
    public void Options()
    {
    }
}