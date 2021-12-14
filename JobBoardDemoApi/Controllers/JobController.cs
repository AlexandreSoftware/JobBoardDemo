using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Net;
using JobBoardServices;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Serilog.Core;
using JobBoardDemoApi.Controllers.Interface;
using Serilog;

namespace JobBoardDemoApi.Controllers;

[ApiController]
[Route("Job")]
public class JobController : Controller, IJobController
{
    public IJobService js;
    public JobController(IJobService js)
    {
        this.js = js;
    }

    [HttpGet]
    public ActionResult<Job[]> Get()
    {
        Log.Information("[JobBoardDemoApi] [JobController] [Get] Starting Get Request");
        var result = js.Get();
        Log.Information("[JobBoardDemoApi] [JobController] [Get] Finished Get Request, Validating");
        if (result != null )
        {
            if (result.Length != 0)
            {
                Log.Information("[JobBoardDemoApi] [JobController] [Get] Validated Get Request, returning");
                return Ok(result);
            }
        }
        Log.Information("[JobBoardDemoApi] [JobController] [Get] [ERROR] Error on request, returning error");
        return NotFound(null);
    }
    
    [HttpGet("{id}")]

    public ActionResult<Job> GetId(int id)
    {
        Log.Information("[JobBoardDemoApi] [JobController] [GetId] Starting GetId request");
        var result = js.GetId(id);
        Log.Information("[JobBoardDemoApi] [JobController] [GetId] Finished GetId request, Validating");
        if (result != null)
        {
            Log.Information("[JobBoardDemoApi] [JobController] [GetId] Validated GetId request, returning");
            return Ok(result);
        }
        else
        {
        
            Log.Information("[JobBoardDemoApi] [JobController] [GetId] [ERROR] Error on request, returning error");
            return NotFound(null);
        }
    }
    [HttpPost]
    public ActionResult<bool> Post(Job j)
    {
        Log.Information("[JobBoardDemoApi] [JobController] [Post] Starting Post request");
        var result = js.Post(j);
        Log.Information("[JobBoardDemoApi] [JobController] [Post] Finished Post request, Validating");
        if (result)
            {
            Log.Information("[JobBoardDemoApi] [JobController] [Post] Validated Post request, returning");
            return true;
            }
        else
        {
            Log.Information("[JobBoardDemoApi] [JobController] [Post] [ERROR] Error on request, returning error");       
            return NotFound(false);
        }
    }
    [HttpDelete]
    public ActionResult<bool> Delete(int id)
    {
        Log.Information("[JobBoardDemoApi] [JobController] [Delete] Starting Delete request");
        var result = js.Delete(id);
        Log.Information("[JobBoardDemoApi] [JobController] [Delete] Finished Delete request, Validating");
        if (result)
        {
            Log.Information("[JobBoardDemoApi] [JobController] [Delete] Validated Delete request, returning");
            //for some reason whenever i use OK(true) it breaks the code, when the code's like this *it just works*
            return true;
            
        }
        else
        {
            Log.Information("[JobBoardDemoApi] [JobController] [Delete] [ERROR] Error on request, returning error");
            return NotFound(false);
        }
    }
    [HttpPut]
    public ActionResult<bool> Put(Job j)
    {
        Log.Information("[JobBoardDemoApi] [JobController] [Put] Starting Put request");
        var result = js.Put(j);
        Log.Information("[JobBoardDemoApi] [JobController] [Put] Finished Put request, Validating");
        if (result)
        {
            Log.Information("[JobBoardDemoApi] [JobController] [Put] Validated Put request, returning");
            return result;
        }
        else
        {
            Log.Information("[JobBoardDemoApi] [JobController] [Put] [ERROR] Error on request, returning error");
            return NotFound(false);
        }
    }
    [HttpOptions]
    public ActionResult Options()
    {
        Log.Information("[JobBoardDemoApi] [JobController] [Options] adding headers");
        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET,DELETE,POST,PUT");
        Log.Information("[JobBoardDemoApi] [JobController] [Options] Returning ");
        return Ok();

    }
    
}