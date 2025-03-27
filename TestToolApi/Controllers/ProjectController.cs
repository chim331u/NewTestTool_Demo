using DataModel;
using DataModel.DTO;
using Microsoft.AspNetCore.Mvc;
using TestToolApi.Interfaces;

namespace TestToolApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{

    private readonly IDataInterface _service;
    private readonly IConfiguration _config;
    private readonly ILogger<ProjectController> _logger;

    public ProjectController(IConfiguration config, ILogger<ProjectController> logger, IDataInterface service)
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
    
    [HttpGet("GetDTOProjectList")]
    public async Task<ActionResult<IEnumerable<DTO_ProjectInfo>>> GetDTOProjectList()
    {
        var projects = await _service.GetDTOProjectList();

        if (projects == null)
        {
            return NotFound();
        }

        return Ok(projects);
    }
    
    [HttpGet("GetProject/{id}")]
    public async Task<ActionResult<Projects>> GetProject(int id)
    {
        var project = await _service.GetProject(id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }
    
    [HttpPost("AddProject")]
    public async Task<ActionResult<Projects>> AddProject(Projects project)
    {
        var newProject = await _service.CreateProject(project);

        return CreatedAtAction("GetProject", new { id = newProject.Id }, newProject);
    }
    
    [HttpPut("UpdateProject/{id}")]
    public async Task<IActionResult> UpdateProject(int id, Projects project)
    {
        if (id != project.Id)
        {
            return BadRequest();
        }

        await _service.UpdateProject(project);

        return NoContent();
    }
    
    [HttpDelete("DeleteProject/{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _service.GetProject(id);

        if (project == null)
        {
            return NotFound();
        }

        await _service.DeleteProject(id);

        return NoContent();
    }
    
}