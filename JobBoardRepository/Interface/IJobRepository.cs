using JobBoardRepository.Domain;

namespace JobBoardRepository.Interface;

public interface IJobRepository
{
        public Task<JobDTO[]> Get();
        public Task<bool> Post(JobDTO j);
        public Task<JobDTO> GetId(int id);
        public Task<bool> Delete(int id);
        public Task<bool> Put(JobDTO j);
}