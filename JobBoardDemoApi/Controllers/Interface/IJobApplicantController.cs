using JobBoardServices.View;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardDemoApi.Controllers.Interface;

public interface IJobApplicantController
{
    public Task<ActionResult<Job[]>> GetAppliedJobs(int aid);
    public Task<ActionResult<bool>> InsertJobApplicant(int jid, int aid);
    public Task<ActionResult<bool>> DeleteJobApplicant(int jaid);
}