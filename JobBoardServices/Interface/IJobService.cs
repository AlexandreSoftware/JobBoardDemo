using JobBoardServices.View;

namespace JobBoardServices.Interface;

public interface IJobService
{
    public Task<Job[]> Get();
    public Task<bool> Post(Job j);
    public Task<Job> GetId(int id);
    public Task<bool> Delete(int id);
    public Task<bool> Put(Job j);
}