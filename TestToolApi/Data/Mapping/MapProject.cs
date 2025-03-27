using AutoMapper;
using DataModel;
using DataModel.DTO;

namespace TestToolApi.Data.Mapping;

public class MapProject : Profile
{
    public MapProject()
    {
        // Source: Project and Destination: Dto_Project
        CreateMap<Projects, DTO_ProjectInfo>();

        // Source: Dto_Project and Destination: Project
        CreateMap<DTO_ProjectInfo, Projects>();
    }
}