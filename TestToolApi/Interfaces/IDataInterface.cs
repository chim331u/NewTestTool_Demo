using DataModel;

namespace TestToolApi.Interfaces;

public interface IDataInterface
{
    #region Project

    Task<List<Projects>> GetProjectList();
    Task<Projects> GetProject(int id);
    Task<Projects> CreateProject(Projects project);
    Task<Projects> UpdateProject(Projects project);
    Task<Projects> DeleteProject(int id);

    #endregion
    
    #region Suites
    Task<List<TestSuites>> GetSuitesList();
    Task<List<TestSuites>> GetSuitesList(int projectId);
    Task<TestSuites> GetSuite(int id);
    Task<TestSuites> CreateSuite(TestSuites suite);
    Task<TestSuites> UpdateSuite(TestSuites suite);
    Task<TestSuites> DeleteSuite(int id);
    #endregion
    
    #region Cases
    Task<List<TestCases>> GetCasesList();
    Task<List<TestCases>> GetCasesList(int suiteId);
    Task<TestCases> GetCase(int id);
    Task<TestCases> CreateCase(TestCases testCase);
    Task<TestCases> UpdateCase(TestCases testCase);
    Task<TestCases> Deletecase(int id);
    #endregion
    
    #region Scripts
    Task<List<TestScripts>> GetScriptList();
    Task<List<TestScripts>> GetScriptList(int testCaseId);
    Task<TestScripts> GetScript(int id);
    Task<TestScripts> CreateScript(TestScripts script);
    Task<TestScripts> UpdateScript(TestScripts script);
    Task<TestScripts> DeleteScript(int id);
    #endregion
}