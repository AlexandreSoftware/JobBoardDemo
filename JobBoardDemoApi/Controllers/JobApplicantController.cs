using JobBoardDemoApi.Controllers.Interface;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace JobBoardDemoApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class JobApplicantController:Controller,IJobApplicantController
{
    private readonly IJobApplicantService _ajs;
    
    public JobApplicantController(IJobApplicantService ajs)
    {
        _ajs = ajs;
    }
    [HttpGet]
    public async Task<ActionResult<Job[]>> GetAppliedJobs(int aid)
    {
        try
        {
            string templateLog = "[JobBoardDemoApi] [ApplicantController] [GetAppliedJobs]";
            Log.Information($"{templateLog} Starting GET Request");
            var result =  await _ajs.GetAppliedJobs(aid);
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
    [HttpPut]
    public async Task<ActionResult<bool>> InsertJobApplicant(int jid, int aid)
    {
        try
        {
            string templateLog = "[JobBoardDemoApi] [ApplicantController] [PUT] ";
            Log.Information($"{templateLog} Starting Put request");
            bool result = await _ajs.InsertJobApplicant(jid,aid);
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
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteJobApplicant(int jaid)
    {
        try
        {
            string templateLog = "[JobBoardDemoApi] [ApplicantController] [DeleteJobApplicant]";
            Log.Information($"{templateLog} Starting Delete request");
            bool result = await _ajs.DeleteJobApplicant(jaid);
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
}