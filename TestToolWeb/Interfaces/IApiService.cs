using DataModel;

namespace TestToolWeb.Interfaces;

public interface IApiService
{
    #region Projects
    Task<List<Projects>> GetProjectList();
    Task<Projects> GetProject(int id);
    
    Task<Projects> UpdateProject(int id, Projects item);
    Task<Projects> AddProject(Projects item);
    Task<Projects> DeleteProject(int id);

    #endregion   
    
    #region Suites
    
    Task<List<TestSuites>> GetSuiteList();
    Task<List<TestSuites>> GetSuitesListByProject(int projectId);
    Task<TestSuites> GetSuite(int id);
    Task<TestSuites> UpdateSuite(int id, TestSuites item);
    Task<TestSuites> AddSuite(TestSuites item);
    Task<TestSuites> DeleteSuite(int id);
    #endregion
    
    #region TestCases
    Task<List<TestCases>> GetTestCaseList();
    Task<List<TestCases>> GetTestCaseListBySuite(int suiteId);
    Task<List<TestCases>> GetTestCaseListByProject(int projectId);
    Task<TestCases> GetTestCase(int id);
    Task<TestCases> UpdateTestCase(int id, TestCases item);
    Task<TestCases> AddTestCase(TestCases item);
    Task<TestCases> DeleteTestCase(int id);
    #endregion
    
    #region TestScripts
    Task<List<TestScripts>> GetTestScriptList();
    Task<List<TestScripts>> GetTestScriptListByCase(int caseId);
    Task<TestScripts> GetTestScript(int id);
    Task<TestScripts> UpdateTestScript(int id, TestScripts item);
    Task<TestScripts> AddTestScript(TestScripts item);
    Task<TestScripts> DeleteTestScript(int id);
    #endregion
}