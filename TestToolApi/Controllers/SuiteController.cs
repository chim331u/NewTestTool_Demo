using DataModel;
using Microsoft.AspNetCore.Mvc;
using TestToolApi.Interfaces;

namespace TestToolApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuiteController :ControllerBase
{
    private readonly IDataInterface _service;
    private readonly IConfiguration _config;
    private readonly ILogger<ProjectController> _logger;

    public SuiteController(IDataInterface service, IConfiguration config, ILogger<ProjectController> logger)
    {
        _service = service;
        _config = config;
        _logger = logger;
    }

    [HttpGet("GetSuitesList")]
    public async Task<ActionResult<IEnumerable<TestSuites>>> GetSuitesList()
    {
        var _suitesList = await _service.GetSuitesList();

        if (_suitesList == null)
        {
            return NotFound();
        }
        
        return  Ok(_suitesList);
        
    }
    
    [HttpGet("GetSuitesListByProject/{projectId}")]
    public async Task<ActionResult<IEnumerable<TestSuites>>> GetSuitesListByProject(int projectId)
    {
        var _suitesList = await _service.GetSuitesList(projectId);

        if (_suitesList == null)
        {
            return NotFound();
        }
        
        return  Ok(_suitesList);
        
    }
    
    [HttpGet("GetSuite/{id}")]
    public async Task<ActionResult<TestSuites>> Getsuite(int id)
    {
        var _suite = await _service.GetSuite(id);

        if (_suite == null)
        {
            return NotFound();
        }

        return Ok(_suite);
    }
    
    [HttpPost("AddSuite")]
    public async Task<ActionResult<TestSuites>> AddSuite(TestSuites suite)
    {
        var _newSuite = await _service.CreateSuite(suite);

        return CreatedAtAction("GetSuite", new { id = _newSuite.Id }, _newSuite);
    }
    
    [HttpPut("UpdateSuite/{id}")]
    public async Task<IActionResult> UpdateSuite(int id, TestSuites suite)
    {
        if (id != suite.Id)
        {
            return BadRequest();
        }

        await _service.UpdateSuite(suite);

        return NoContent();
    }
    
    [HttpDelete("DeleteSuite/{id}")]
    public async Task<IActionResult> DeleteSuite(int id)
    {
        var _suite = await _service.GetSuite(id);

        if (_suite == null)
        {
            return NotFound();
        }

        await _service.DeleteSuite(id);

        return NoContent();
    }
}