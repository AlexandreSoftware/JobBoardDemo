using JobBoardRepository.Domain;

namespace JobBoardRepository.Interface;

public interface IApplicantRepository
{
    public Task<ApplicantDTO[]> Get();
    public Task<bool> Post(ApplicantDTO ap);
    public Task<ApplicantDTO> GetId(int id);
    public Task<bool> Delete(int id);
    public Task<bool> Put(ApplicantDTO ap);
}