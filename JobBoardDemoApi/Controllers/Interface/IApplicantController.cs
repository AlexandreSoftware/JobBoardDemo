using JobBoardRepository.Domain;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardDemoApi.Controllers.Interface;

public interface IApplicantController
{
    public Task<ActionResult<Applicant[]>> Get();
    public Task<ActionResult<Applicant>> GetId(int id);
    public Task<ActionResult<bool>> Post(Applicant ap);
    public Task<ActionResult<bool>> Put(Applicant ap);
    public Task<ActionResult<bool>> Delete(int id);
    public Task<ActionResult> Options();
}