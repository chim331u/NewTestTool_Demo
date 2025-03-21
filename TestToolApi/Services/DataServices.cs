using DataModel;
using Microsoft.EntityFrameworkCore;
using TestToolApi.Data;
using TestToolApi.Interfaces;

namespace TestToolApi.Services;

public class DataServices : IDataInterface
{
    private readonly DataContext _context;
    private readonly IConfiguration _config;
    private readonly ILogger<DataServices> _logger;
    
    public DataServices(IConfiguration config, ILogger<DataServices> logger, DataContext context)
    {
        _context = context;
        _config = config;
        _logger = logger;
    }
    
    #region Projects
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
            _logger.LogError($"Error retrieve projects {ex.Message}");
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
            _logger.LogError($"Error retrieve project {ex.Message}");
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
            _context.Entry(project).State = EntityState.Modified;
            
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
            var project = await _context.Projects.FindAsync(id);
            project.IsActive = false;
            project.ModifiedDate = DateTime.Now;
            _context.Entry(project).State = EntityState.Modified;
            
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


    #endregion

    #region Suites

    
    public async Task<List<TestSuites>> GetSuitesList()
    {
        try
        {
            var _suiteList = await _context.TestSuites.Where(c => c.IsActive).ToListAsync(); ;
            _logger.LogInformation("Get all Suites");
            return _suiteList;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieve suites {ex.Message}");
            return null;
        }
    }

    public async Task<List<TestSuites>> GetSuitesList(int projectId)
    {
        try
        {
            var _suiteList = await _context.TestSuites.Where(c => c.IsActive && c.Project.Id == projectId).ToListAsync(); ;
            _logger.LogInformation($"Get all Suites for project id: {projectId}");
            return _suiteList;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieve suites for Prj {ex.Message}");
            return null;
        }
    }
    public async Task<TestSuites> GetSuite(int id)
    {
        try
        {
            var _suite = await _context.TestSuites.Where(c => c.Id == id).FirstOrDefaultAsync();
            _logger.LogInformation($"Get suite by id: {id}");
            return _suite;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieve suite {ex.Message}");
            return null;
        }
    }

    public async Task<TestSuites> CreateSuite(TestSuites suite)
    {
        try
        {
            suite.IsActive = true;
            suite.CreatedDate = DateTime.Now;
            _context.TestSuites.Add(suite);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Created suite: {suite.RequirementName}");
            return suite;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error create suite {ex.Message}");
            return null;
        }
    }

    public async Task<TestSuites> UpdateSuite(TestSuites suite)
    {
        suite.ModifiedDate = DateTime.Now;
        _context.Entry(suite).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated suite: {suite.RequirementName}");
        }
        catch (DbUpdateConcurrencyException ex)
        {
           _logger.LogError($"Error updating suite {ex.Message}");
           return null;
        }

        return suite;
    }

    public async Task<TestSuites> DeleteSuite(int id)
    {
        try
        {
            var _suite = await _context.TestSuites.FindAsync(id);
            
            _suite.IsActive = false;
            _suite.ModifiedDate = DateTime.Now;
            
            _context.Entry(_suite).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted suite: {_suite.RequirementName}");
            return _suite;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error delete suite {ex.Message}");
            return null;
        }
    }


    #endregion

    #region TestCase

    
    public async Task<List<TestCases>> GetCasesList()
    {
        throw new NotImplementedException();
    }

    public async Task<List<TestCases>> GetCasesList(int suiteId)
    {
        throw new NotImplementedException();
    }

    public async Task<TestCases> GetCase(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<TestCases> CreateCase(TestCases testCase)
    {
        throw new NotImplementedException();
    }

    public async Task<TestCases> UpdateCase(TestCases testCase)
    {
        throw new NotImplementedException();
    }

    public async Task<TestCases> Deletecase(int id)
    {
        throw new NotImplementedException();
    }

    #endregion
    
    #region TestScripts
    public async Task<List<TestScripts>> GetScriptList()
    {
        throw new NotImplementedException();
    }

    public async Task<List<TestScripts>> GetScriptList(int testCaseId)
    {
        throw new NotImplementedException();
    }

    public async Task<TestScripts> GetScript(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<TestScripts> CreateScript(TestScripts script)
    {
        throw new NotImplementedException();
    }

    public async Task<TestScripts> UpdateScript(TestScripts script)
    {
        throw new NotImplementedException();
    }

    public async Task<TestScripts> DeleteScript(int id)
    {
        throw new NotImplementedException();
    }
    #endregion
}