using AutoMapper;
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
        }
    }
}