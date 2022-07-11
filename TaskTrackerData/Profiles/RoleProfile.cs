
using AutoMapper;

namespace TaskTrackerData.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Domain.Role, Common.Contract.Model.RoleDto>();
        }
    }
}
