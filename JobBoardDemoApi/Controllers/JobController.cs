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
    public IJobService _js;
    public JobController(IJobService js)
    {
        this._js = js;
    }

    [HttpGet]
    public async Task<ActionResult<Job[]>> Get()
    {
        Log.Information("[JobBoardDemoApi] [JobController] [Get] Starting Get Request");
        var result =  await _js.Get();
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

    public async Task<ActionResult<Job>> GetId(int id)
    {
        string templateLog = "[JobBoardDemoApi] [JobController] [GetId] "; 
        Log.Information($"{templateLog} Starting GetId request");
        Job? result = await _js.GetId(id);
        Log.Information($"{templateLog} Finished GetId request, Validating");
        if (result != null)
        {
            Log.Information($"{templateLog} Validated GetId request, returning");
            return Ok(result);
        }
        else
        {
        
            Log.Information($"{templateLog} Error on request, returning error");
            return NotFound(null);
        }
    }
    [HttpPost]
    public async Task<ActionResult<bool>> Post(Job j)
    {
        Log.Information("[JobBoardDemoApi] [JobController] [Post] Starting Post request");
        var result = await _js.Post(j);
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
    public async Task<ActionResult<bool>> Delete(int id)
    {
        Log.Information("[JobBoardDemoApi] [JobController] [Delete] Starting Delete request");
        bool result = await _js.Delete(id);
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
    public async Task<ActionResult<bool>> Put(Job j)
    {
        Log.Information("[JobBoardDemoApi] [JobController] [Put] Starting Put request");
        bool result = await _js.Put(j);
        Log.Information("[JobBoardDemoApi] [JobController] [Put] Finished Put request, Validating");
        if (result)
        {
            Log.Information("[JobBoardDemoApi] [JobController] [Put] Validated Put request, returning");
            return true;
        }
        else
        {
            Log.Information("[JobBoardDemoApi] [JobController] [Put] [ERROR] Error on request, returning error");
            return NotFound(false);
        }
    }
    [HttpOptions]
    public async Task<ActionResult> Options()
    {
        Log.Information("[JobBoardDemoApi] [JobController] [Options] adding headers");
        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET,DELETE,POST,PUT");
        Log.Information("[JobBoardDemoApi] [JobController] [Options] Returning ");
        return Ok();

    }
    
}