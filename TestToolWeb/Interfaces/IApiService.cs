using DataModel;

namespace TestToolWeb.Interfaces;

public interface IApiService
{
    Task<List<Projects>> GetProjectList();
    Task<Projects> GetProject(int id);
    
    Task<Projects> UpdateConfig(Projects item);
    Task<Projects> AddConfig(Projects item);
    Task<Projects> DeleteConfig(int id);
    
    
}