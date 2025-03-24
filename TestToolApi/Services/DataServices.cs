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
            var projectList = await _context.Projects.AsNoTracking()
                .Include(x=>x.TestSuites).ThenInclude(x=>x.TestCases)
                .Where(c => c.IsActive).ToListAsync();
            ;
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
            var _suiteList = await _context?.TestSuites
                .AsNoTracking()
                .Include(a => a.Project)?
                .Where(c => c.IsActive).ToListAsync()!;
            ;
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
            var _suiteList = await _context?.TestSuites?
                .AsNoTracking()
                .Include(a => a.Project)?
                .Where(c => c.IsActive && c.Project.Id == projectId).ToListAsync()!;

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
            var prj = await _context.Projects.FindAsync(suite.Project.Id);
            suite.IsActive = true;
            suite.CreatedDate = DateTime.Now;
            suite.Project=prj;
            
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
        try
        {
            var _caseList = await _context.TestCases
                .AsNoTracking()
                .Include(a=>a.TestSuite)
                .Include(a=>a.TestSuite.Project)
                .Where(c => c.IsActive).ToListAsync();
            _logger.LogInformation("Get all TestCases");
            return _caseList;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieve TestCases {ex.Message}");
            return null;
        }
    }

    public async Task<List<TestCases>> GetCasesList(int suiteId)
    {
        try
        {
            var _caseList = await _context.TestCases
                .AsNoTracking()
                .Include(a=>a.TestSuite)
                .Include(a=>a.TestSuite.Project)
                .Where(c => c.IsActive && c.TestSuite.Id == suiteId).ToListAsync();
            _logger.LogInformation($"Get all TestCases for suite id: {suiteId}");
            return _caseList;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieve TestCases for suite {ex.Message}");
            return null;
        }
    }

    public async Task<List<TestCases>> GetCasesListByProject(int projectId)
    {
        try
        {
            var _caseList = await _context.TestCases
                .AsNoTracking()
                .Include(a=>a.TestSuite)
                .Include(a=>a.TestSuite.Project)
                .Where(c => c.IsActive && c.TestSuite.Project.Id == projectId).ToListAsync();
            _logger.LogInformation($"Get all TestCases for project id: {projectId}");
            return _caseList;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieve TestCases for suite {ex.Message}");
            return null;
        }
    }
    public async Task<TestCases> GetCase(int id)
    {
        try
        {
            var _case = await _context.TestCases.FindAsync(id);
            _logger.LogInformation($"Get TestCases by id: {id}");
            return _case;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieve TestCases {ex.Message}");
            return null;
        }
    }

    public async Task<TestCases> CreateCase(TestCases testCase)
    {
        try
        {
            var suite = await _context.TestSuites.FindAsync(testCase.TestSuite.Id);
            var project = await _context.Projects.FindAsync(testCase.TestSuite.Project.Id);
            testCase.TestSuite = suite;
            testCase.TestSuite.Project = project;
            
            testCase.IsActive = true;
            testCase.CreatedDate = DateTime.Now;
            _context.TestCases.Add(testCase);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Created TestCases: {testCase.TestCaseName}");
            return testCase;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error create TestCases {ex.Message}");
            return null;
        }
    }

    public async Task<TestCases> UpdateCase(TestCases testCase)
    {
        try
        {
            testCase.ModifiedDate = DateTime.Now;
            _context.Entry(testCase).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated TestCases: {testCase.TestCaseName}");
            return testCase;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error update TestCases {ex.Message}");
            return null;
        }
    }

    public async Task<TestCases> Deletecase(int id)
    {
        try
        {
            var _case = await _context.TestCases.FindAsync(id);
            _case.IsActive = false;
            _case.ModifiedDate = DateTime.Now;
            _context.Entry(_case).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted TestCases: {_case.TestCaseName}");
            return _case;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error delete TestCases {ex.Message}");
            return null;
        }
    }

    #endregion

    #region TestScripts

    public async Task<List<TestScripts>> GetScriptList()
    {
        try
        {
            var _scriptList = await _context.TestScripts.Where(c => c.IsActive).ToListAsync();
            _logger.LogInformation("Get all TestScripts");
            return _scriptList;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieve TestScripts {ex.Message}");
            return null;
        }
    }

    public async Task<List<TestScripts>> GetScriptList(int testCaseId)
    {
        try
        {
            var _scriptList = await _context.TestScripts.Where(c => c.IsActive && c.TestCase.Id == testCaseId)
                .ToListAsync();
            _logger.LogInformation($"Get all TestScripts for TestCases id: {testCaseId}");
            return _scriptList;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieve TestScripts for TestCases {ex.Message}");
            return null;
        }
    }

    public async Task<TestScripts> GetScript(int id)
    {
        try
        {
            var _script = await _context.TestScripts.FindAsync(id);
            _logger.LogInformation($"Get TestScripts by id: {id}");
            return _script;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieve TestScripts {ex.Message}");
            return null;
        }
    }

    public async Task<TestScripts> CreateScript(TestScripts script)
    {
        try
        {
            script.IsActive = true;
            script.CreatedDate = DateTime.Now;
            _context.TestScripts.Add(script);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Created TestScripts: {script.Id}");
            return script;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error create TestScripts {ex.Message}");
            return null;
        }
    }

    public async Task<TestScripts> UpdateScript(TestScripts script)
    {
        try
        {
            script.ModifiedDate = DateTime.Now;
            _context.Entry(script).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated TestScripts: {script.Id}");
            return script;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error update TestScripts {ex.Message}");
            return null;
        }
    }

    public async Task<TestScripts> DeleteScript(int id)
    {
        try
        {
            var _script = await _context.TestScripts.FindAsync(id);
            _script.IsActive = false;
            _script.ModifiedDate = DateTime.Now;
            _context.Entry(_script).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted TestScripts: {_script.Id}");
            return _script;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error delete TestScripts {ex.Message}");
            return null;
        }
    }

    #endregion
}