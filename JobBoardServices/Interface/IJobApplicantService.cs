using JobBoardServices.View;

namespace JobBoardServices.Interface;

public interface IJobApplicantService {
    public Task<Job[]> GetAppliedJobs(int aid);
    public Task<bool> InsertJobApplicant(int jid, int aid);
    public Task<bool> DeleteJobApplicant(int jaid);
}