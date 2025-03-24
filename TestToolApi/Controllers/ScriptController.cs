using DataModel;
using Microsoft.AspNetCore.Mvc;
using TestToolApi.Interfaces;

namespace TestToolApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScriptController: ControllerBase
{
    private readonly ILogger<ScriptController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IDataInterface _service;
    
    public ScriptController(ILogger<ScriptController> logger, IConfiguration configuration, IDataInterface service)
    {
        _logger = logger;
        _configuration = configuration;
        _service = service;
    }
    
    [HttpGet("GetScriptList")]
    public async Task<ActionResult<IEnumerable<TestScripts>>> GetScriptList()
    {
        var scripts = await _service.GetScriptList();
        
        if (scripts == null)
        {
            return NotFound();
        }
        
        return Ok(scripts);
    }
    
    [HttpGet("GetScriptListByCase/{caseId}")]
    private async Task<ActionResult<IEnumerable<TestScripts>>> GetScriptListByCase(int caseId)
    {
        var scripts = await _service.GetScriptList(caseId);
        
        if (scripts == null)
        {
            return NotFound();
        }
        
        return Ok(scripts);
    }
    
    [HttpGet("GetScript/{id}")]
    public async Task<ActionResult<TestScripts>> GetScript(int id)
    {
        var script = await _service.GetScript(id);
        
        if (script == null)
        {
            return NotFound();
        }
        
        return Ok(script);
    }
    
    [HttpPost("AddScript")]
    public async Task<ActionResult<TestScripts>> AddScript(TestScripts script)
    {
        var newScript = await _service.CreateScript(script);
        
        if (newScript == null)
        {
            return BadRequest();
        }
        
        return Ok(newScript);
    }
    
    [HttpPut("UpdateScript/{id}")]
    public async Task<ActionResult<TestScripts>> UpdateScript(int id, TestScripts script)
    {
        if (id != script.Id)
        {
            return BadRequest();
        }
        
        var updatedScript = await _service.UpdateScript(script);
        
        if (updatedScript == null)
        {
            return BadRequest();
        }
        
        return Ok(updatedScript);
    }
    
    [HttpDelete("DeleteScript/{id}")]
    public async Task<ActionResult<TestScripts>> DeleteScript(int id)
    {
        var script = await _service.GetScript(id);
        
        if (script == null)
        {
            return NotFound();
        }
        
        await _service.DeleteScript(id);
        
        return Ok(script);
    }
    
}