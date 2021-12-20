using JobBoardRepository.Domain;

namespace JobBoardRepository.Interface;

public interface IJobRepository
{
        public JobDTO[] Get();
        public bool Post(JobDTO j);
        public JobDTO GetId(int id);
        public bool Delete(int id);
        public bool Put(JobDTO j);
        public void Options();
}