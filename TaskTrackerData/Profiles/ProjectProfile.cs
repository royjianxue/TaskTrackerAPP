using AutoMapper;

namespace TaskTrackerData.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Domain.Project, Common.Contract.Model.ProjectDto>();
            CreateMap<Common.Contract.Model.ProjectForUpdateDto, Domain.Project>();
            CreateMap<Domain.Project, Common.Contract.Model.ProjectDto>();
        }
    }
}
