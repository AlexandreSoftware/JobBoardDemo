using JobBoardServices.View;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardDemoApi.Controllers.Interface;

public interface IJobController
{
    public ActionResult<Job[]> Get();
    public ActionResult<bool> Post(Job j);
    public ActionResult<Job> GetId(int id);
    public ActionResult<bool> Delete(int id);
    public ActionResult<bool> Put(Job j);
    public ActionResult Options();
}