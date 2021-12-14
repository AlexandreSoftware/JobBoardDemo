using JobBoardServices.View;

namespace JobBoardServices.Interface;

public interface IJobService
{
    public Job[] Get();
    public bool Post(Job j);
    public Job GetId(int id);
    public bool Delete(int id);
    public bool Put(Job j);
    public void Options();

}