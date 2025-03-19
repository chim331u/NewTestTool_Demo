using DataModel;

namespace TestToolApi.Interfaces;

public interface IprojectInterface
{
    Task<List<Projects>> GetProjectList();
}