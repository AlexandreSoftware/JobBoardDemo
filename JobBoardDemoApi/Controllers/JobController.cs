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
        try
        {
            string templateLog = "[JobBoardDemoApi] [JobController] [GET]";
            Log.Information($"{templateLog} Starting GET Request");
            var result =  await _js.Get();
            Log.Information($"{templateLog} Finished GET Request, Validating");
            if (result != null)
            {
                if (result.Length != 0)
                {
                    Log.Information($"{templateLog} Validated GET request, returning");
                    return Ok(result);
                }
            }

            Log.Error($"{templateLog} [ERROR] Empty Request, returning Null");
            return NotFound(null);
        }
        catch (Exception e)
        {
            Log.Error("[ERROR] exception catched "+e.Message);
            return NotFound(null);
        }
    }
    
    [HttpGet("{id}")]

    public async Task<ActionResult<Job>> GetId(int id)
    {
        try
        {
            string templateLog = "[JobBoardDemoApi] [JobController] [GETId] ";
            Log.Information($"{templateLog} Starting GETId request");
            Job? result = await _js.GetId(id);
            Log.Information($"{templateLog} Finished GETId request, Validating");
            if (result != null)
            {
                Log.Information($"{templateLog} Validated GETId request, returning");
                return Ok(result);
            }

            Log.Information($"{templateLog} Error on request, returning error");
            return NotFound(null);
        }
        catch (Exception e)
        {
            Log.Error("[ERROR] exception catched "+e.Message);
            return NotFound(null);
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> Post(Job j)
    {
        try
        {
            string templateLog = "[JobBoardDemoApi] [JobController] [POST] ";
            Log.Information($"{templateLog} Starting Post request");
            var result = await _js.Post(j);
            Log.Information($"{templateLog} Finished Post request, Validating");
            if (result)
            {
                Log.Information($"{templateLog} Validated Post request, returning");
                return true;
            }
            else
            {
                Log.Information($"{templateLog} [ERROR] Error on request, returning error");
                return NotFound(false);
            }
        }
        catch (Exception e)
        {
            Log.Error("[ERROR] exception catched "+e.Message);
            return NotFound(false);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        try
        {
            string templateLog = "[JobBoardDemoApi] [JobController] [DELETE] ";
            Log.Information($"{templateLog} Starting Delete request");
            bool result = await _js.Delete(id);
            Log.Information($"{templateLog} Finished Delete request, Validating");
            if (result)
            {
                Log.Information($"{templateLog} Validated Delete request, returning");
                //for some reason whenever i use OK(true) it breaks the code, when the code's like this *it just works*
                return true;

            }
            else
            {
                Log.Information($"{templateLog} [ERROR] Error on request, returning error");
                return NotFound(false);
            }
        }
        catch (Exception e)
        {
            Log.Error("[ERROR] exception catched "+e.Message);
            return NotFound(false);
        }
    }
    [HttpPut]
    public async Task<ActionResult<bool>> Put(Job j)
    {
        try
        {
            string templateLog = "[JobBoardDemoApi] [JobController] [PUT] ";
            Log.Information($"{templateLog} Starting Put request");
            bool result = await _js.Put(j);
            Log.Information($"{templateLog} Finished Put request, Validating");
            if (result)
            {
                Log.Information($"{templateLog} Validated Put request, returning");
                return true;
            }
            else
            {
                Log.Information($"{templateLog} [ERROR] Error on request, returning error");
                return NotFound(false);
            }
        }
        catch (Exception e)
        {
            Log.Error("[ERROR] exception catched "+e.Message);
            return NotFound(null);
        }
    }
    [HttpOptions]
    public async Task<ActionResult> Options()
    {
        try
        {
            string templateLog = "[JobBoardDemoApi] [JobController] [Options] ";
            Log.Information($"{templateLog} adding headers");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET,DELETE,POST,PUT");
            Log.Information($"{templateLog} [Options] Returning ");
            return Ok();
        }
        catch (Exception e)
        {
            Log.Error("[ERROR] exception catched "+e.Message);
            return NotFound();
        }
    }
    
}