using AutoMapper;
using Timeify.Api.Shared.Models.Response;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Entities;
using Timeify.Infrastructure.Context.Identity;

namespace Timeify.Infrastructure.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, AppUser>()
                .ConstructUsing(u => new AppUser {UserName = u.UserName, Email = u.Email})
                .ForMember(au => au.Id, opt => opt.Ignore());

            CreateMap<AppUser, UserEntity>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<UpdateJobTaskRequest, JobTaskEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.FinishDate, opt => opt.MapFrom(src => src.FinishDate))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<UpdateJobRequest, JobEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<JobTaskEntity, JobTask>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobEntityId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.FinishDate, opt => opt.MapFrom(src => src.FinishDate))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}