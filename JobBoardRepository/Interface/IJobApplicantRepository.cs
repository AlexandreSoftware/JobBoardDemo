using JobBoardRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardRepository.Interface;
public interface IJobApplicantRepository
{
    public Task<JobDTO[]> GetAppliedJobs(int aid);
    public Task<bool> InsertJobApplicant(int jid, int aid);
    public Task<bool> DeleteJobApplicant(int jaid);

}