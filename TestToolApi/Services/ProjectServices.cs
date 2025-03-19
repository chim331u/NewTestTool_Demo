using DataModel;
using Microsoft.EntityFrameworkCore;
using TestToolApi.Data;
using TestToolApi.Interfaces;

namespace TestToolApi.Services;

public class ProjectServices : IprojectInterface
{
    private readonly DataContext _context;
    private readonly IConfiguration _config;
    private readonly ILogger<ProjectServices> _logger;
    
    public ProjectServices(IConfiguration config, ILogger<ProjectServices> logger, DataContext context)
    {
        _context = context;
        _config = config;
        _logger = logger;
    }
    public async Task<List<Projects>> GetProjectList()
    {
        try
        {
            var projectList = await _context.Projects.Where(c => c.IsActive).ToListAsync(); ;
            _logger.LogInformation("Get all projects");
            return projectList;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrive projects {ex.Message}");
            return null;
        }
    }
    
    public async Task<Projects> GetProject(int id)
    {
        try
        {
            var project = await _context.Projects.Where(c => c.Id == id).FirstOrDefaultAsync();
            _logger.LogInformation($"Get project by id: {id}");
            return project;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrive project {ex.Message}");
            return null;
        }
    }
    
    public async Task<Projects> CreateProject(Projects project)
    {
        try
        {
            project.IsActive = true;
            project.CreatedDate = DateTime.Now;
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Created project: {project.ProjectName}");
            return project;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error create project {ex.Message}");
            return null;
        }
    }
    
    public async Task<Projects> UpdateProject(Projects project)
    {
        try
        {
            project.ModifiedDate = DateTime.Now;
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Update project: {project.ProjectName}");
            return project;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updated project {ex.Message}");
            return null;
        }
    }
    
    public async Task<Projects> DeleteProject(int id)
    {
        try
        {
            var project = await _context.Projects.Where(c => c.Id == id).FirstOrDefaultAsync();
            project.IsActive = false;
            project.ModifiedDate = DateTime.Now;
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted project: {project.ProjectName}");
            return project;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error delete project {ex.Message}");
            return null;
        }
    }
}