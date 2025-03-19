using DataModel;

namespace TestToolApi.Interfaces;

public interface IprojectInterface
{
    Task<List<Projects>> GetProjectList();
    Task<Projects> GetProject(int id);
    Task<Projects> CreateProject(Projects project);
    Task<Projects> UpdateProject(Projects project);
    Task<Projects> DeleteProject(int id);
}