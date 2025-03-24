using DataModel;
using Microsoft.AspNetCore.Mvc;
using TestToolApi.Interfaces;

namespace TestToolApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CasesController :ControllerBase
{
    private readonly ILogger<CasesController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IDataInterface _service;
    
    public CasesController(ILogger<CasesController> logger, IConfiguration configuration, IDataInterface service)
    {
        _logger = logger;
        _configuration = configuration;
        _service = service;
    }
    
    [HttpGet("GetCaseList")]
    public async Task<ActionResult<IEnumerable<TestCases>>> GetCaseList()
    {
        var cases = await _service.GetCasesList();
        
        if (cases == null)
        {
            return NotFound();
        }
        
        return Ok(cases);
    }
    
    [HttpGet("GetCaseListBySuite/{suiteId}")]
    public async Task<ActionResult<IEnumerable<TestCases>>> GetCaseListBySuite(int suiteId)
    {
        var cases = await _service.GetCasesList(suiteId);
        
        if (cases == null)
        {
            return NotFound();
        }
        
        return Ok(cases);
    }
    
    [HttpGet("GetCaseListByProject/{projectId}")]
    public async Task<ActionResult<IEnumerable<TestCases>>> GetCaseListByProject(int projectId)
    {
        var cases = await _service.GetCasesListByProject(projectId);
        
        if (cases == null)
        {
            return NotFound();
        }
        
        return Ok(cases);
    }
    [HttpGet("GetCase/{id}")]
    public async Task<ActionResult<TestCases>> GetCase(int id)
    {
        var cases = await _service.GetCase(id);
        
        if (cases == null)
        {
            return NotFound();
        }
        
        return Ok(cases);
    }
    
    [HttpPost("AddCase")]
    public async Task<ActionResult<TestCases>> AddCase(TestCases cases)
    {
        var newCase = await _service.CreateCase(cases);
        
        if (newCase == null)
        {
            return BadRequest();
        }
        
        return Ok(newCase);
    }
    
    [HttpPut("UpdateCase/{id}")]
    public async Task<ActionResult<TestCases>> UpdateCase(int id, TestCases cases)
    {
        if (id != cases.Id)
        {
            return BadRequest();
        }
        var updatedCase = await _service.UpdateCase(cases);
        
        if (updatedCase == null)
        {
            return BadRequest();
        }
        
        return Ok(updatedCase);
    }
    
    [HttpDelete("DeleteCase/{id}")]
    public async Task<ActionResult<TestCases>> DeleteCase(int id)
    {
        var deletedCase = await _service.Deletecase(id);
        
        if (deletedCase == null)
        {
            return BadRequest();
        }
        
        return Ok(deletedCase);
    }
    
}