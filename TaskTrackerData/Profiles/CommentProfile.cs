using AutoMapper;

namespace TaskTrackerData.Profiles
{
    public class CommentProfile : Profile
    {

        public CommentProfile()
        {
            CreateMap<Common.Contract.Model.CommentDto, Domain.Comment>();

            CreateMap<Domain.Comment, Common.Contract.Model.CommentDto>();

            CreateMap<Common.Contract.Model.CommentForUpdateDto, Domain.Comment>();

        }

    }
}
