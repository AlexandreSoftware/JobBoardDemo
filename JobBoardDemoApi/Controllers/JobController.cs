using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using JobBoardServices.View;

namespace JobBoardDemoApi.Controllers;
[ApiController]
[Route("Job")]
public class JobController : Controller
{
    [HttpGet]
    public ActionResult<Job> Get()
    {
      
    }

    [HttpPost]
    public ActionResult<Job> Post()
    {
        return null;
    }
    [HttpDelete]
    public ActionResult<Job> Delete()
    {
        return null;
    }
    [HttpPut]
    public ActionResult<Job> Put()
    {
        return null;
    }
    [HttpOptions]
    public ActionResult<Job> Options()
    {
        return null;
    }
    
}