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
}