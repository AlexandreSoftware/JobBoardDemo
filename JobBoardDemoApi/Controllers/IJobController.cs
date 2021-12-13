using JobBoardServices.View;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardDemoApi.Controllers;

public interface IJobController
{
    public ActionResult<Job> Get();
    public ActionResult<Job> GetId(int id);
    public ActionResult<Job> Post();
    public ActionResult<Job> Delete();
    public ActionResult<Job> Put();
    public ActionResult<Job> Options();
}