using AutoMapper;

namespace TaskTrackerData.Profiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Domain.Permission, Common.Contract.Model.PermissionDto>();
        }


    }
}
