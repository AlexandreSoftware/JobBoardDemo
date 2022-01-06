using JobBoardDemoApi.Controllers.Interface;
using JobBoardRepository.Domain;
using JobBoardServices;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace JobBoardDemoApi.Controllers;

[ApiController]
[Route("Applicant")]
public class ApplicantController : Controller,IApplicantController
{
    public IApplicantService _as;

    public ApplicantController(IApplicantService _as)
    {
        this._as = _as;
    }
    [HttpGet]
    public async Task<ActionResult<Applicant[]>> Get()
    {
        Log.Information("[JobBoardDemoApi] [ApplicantController] [Get] Starting Get Request");
        var result =  await _as.Get();
        Log.Information("[JobBoardDemoApi] [ApplicantController] [Get] Finished Get Request, Validating");
        if (result != null )
        {
            if (result.Length != 0)
            {
                Log.Information("[JobBoardDemoApi] [ApplicantController] [Get] Validated Get Request, returning");
                return Ok(result);
            }
        }
        Log.Information("[JobBoardDemoApi] [ApplicantController] [Get] [ERROR] Error on request, returning error");
        return NotFound(null);
    }
    
    [HttpGet("{id}")]

    public async Task<ActionResult<Applicant>> GetId(int id)
    {
        string templateLog = "[JobBoardDemoApi] [ApplicantController] [GetId] "; 
        Log.Information($"{templateLog} Starting GetId request");
        Applicant? result = await _as.GetId(id);
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
    public async Task<ActionResult<bool>> Post(Applicant j)
    {
        Log.Information("[JobBoardDemoApi] [ApplicantController] [Post] Starting Post request");
        var result = await _as.Post(j);
        Log.Information("[JobBoardDemoApi] [ApplicantController] [Post] Finished Post request, Validating");
        if (result)
            {
            Log.Information("[JobBoardDemoApi] [ApplicantController] [Post] Validated Post request, returning");
            return true;
            }
        else
        {
            Log.Information("[JobBoardDemoApi] [ApplicantController] [Post] [ERROR] Error on request, returning error");       
            return NotFound(false);
        }
    }
    [HttpDelete]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        Log.Information("[JobBoardDemoApi] [ApplicantController] [Delete] Starting Delete request");
        bool result = await _as.Delete(id);
        Log.Information("[JobBoardDemoApi] [ApplicantController] [Delete] Finished Delete request, Validating");
        if (result)
        {
            Log.Information("[JobBoardDemoApi] [ApplicantController] [Delete] Validated Delete request, returning");
            //for some reason whenever i use OK(true) it breaks the code, when the code's like this *it just works*
            return true;
            
        }
        else
        {
            Log.Information("[JobBoardDemoApi] [ApplicantController] [Delete] [ERROR] Error on request, returning error");
            return NotFound(false);
        }
    }
    [HttpPut]
    public async Task<ActionResult<bool>> Put(Applicant j)
    {
        Log.Information("[JobBoardDemoApi] [ApplicantController] [Put] Starting Put request");
        bool result = await _as.Put(j);
        Log.Information("[JobBoardDemoApi] [ApplicantController] [Put] Finished Put request, Validating");
        if (result)
        {
            Log.Information("[JobBoardDemoApi] [ApplicantController] [Put] Validated Put request, returning");
            return true;
        }
        else
        {
            Log.Information("[JobBoardDemoApi] [ApplicantController] [Put] [ERROR] Error on request, returning error");
            return NotFound(false);
        }
    }
    [HttpOptions]
    public async Task<ActionResult> Options()
    {
        Log.Information("[JobBoardDemoApi] [ApplicantController] [Options] adding headers");
        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET,DELETE,POST,PUT");
        Log.Information("[JobBoardDemoApi] [ApplicantController] [Options] Returning ");
        return Ok();

    }
    
}