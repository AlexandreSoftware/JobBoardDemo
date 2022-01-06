using JobBoardServices.View;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardDemoApi.Controllers.Interface;

public interface IJobController
{
    public Task<ActionResult<Job[]>> Get();
    public Task<ActionResult<bool>> Post(Job j);
    public Task<ActionResult<Job>> GetId(int id);
    public Task<ActionResult<bool>> Delete(int id);
    public Task<ActionResult<bool>> Put(Job j);
    public Task<ActionResult> Options();
}