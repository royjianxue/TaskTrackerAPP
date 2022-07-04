using AutoMapper;

namespace TaskTrackerData.Profiles
{
    public class SignUpProfile : Profile
    {
        public SignUpProfile()
        {
            CreateMap<Domain.User, Common.Contract.Model.UserDto>();
        }

    }
}
