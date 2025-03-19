using DataModel;
using Microsoft.AspNetCore.Mvc;
using TestToolApi.Interfaces;

namespace TestToolApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{

    private readonly IprojectInterface _service;
    private readonly IConfiguration _config;
    private readonly ILogger<ProjectController> _logger;

    public ProjectController(IConfiguration config, ILogger<ProjectController> logger, IprojectInterface service)
    {
        _service = service;
        _config = config;
        _logger = logger;
    }
    
    [HttpGet("GetProjectList")]
    public async Task<ActionResult<IEnumerable<Projects>>> GetProjectList()
    {
        var projects = await _service.GetProjectList();

        if (projects == null)
        {
            return NotFound();
        }

        return Ok(projects);
    }
    
}