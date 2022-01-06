using JobBoardRepository.Domain;

namespace JobBoardServices.Interface;

public interface IApplicantService
{
    public Task<Applicant[]> Get();
    public Task<bool> Post(Applicant j);
    public Task<Applicant> GetId(int id);
    public Task<bool> Delete(int id);
    public Task<bool> Put(Applicant j);
}
