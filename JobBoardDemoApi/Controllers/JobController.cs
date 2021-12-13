using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using JobBoardServices;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Serilog.Core;

namespace JobBoardDemoApi.Controllers;

[ApiController]
[Route("Job")]
public class JobController : Controller, IJobController
{
    public IJobService js;
    public Logger l;

    public JobController(IJobService js,
        Logger l)
    {
        this.js = js;
        this.l = l;
    }

    [HttpGet]
    public ActionResult<Job> Get()
    {
        return null;
    }
    
    [HttpGet("{id}")]

    public ActionResult<Job> GetId(int id)
    {
        return null;
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