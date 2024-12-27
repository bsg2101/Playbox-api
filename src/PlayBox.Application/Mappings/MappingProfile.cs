using System;
using AutoMapper;
using PlayBox.Domain.Entities;
using PlayBox.Application.DTOs.Content;
using PlayBox.Application.DTOs.Auth;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayBox.Domain.Enums;

namespace PlayBox.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Content mappings
            CreateMap<Content, ContentDto>();
            CreateMap<CreateContentDto, Content>();
            CreateMap<UpdateContentDto, Content>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Link mappings
            CreateMap<Link, LinkDto>();
            CreateMap<CreateLinkDto, Link>();
            CreateMap<UpdateLinkDto, Link>();

            // Comment mappings
            CreateMap<Comment, CommentDto>();

            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password)) // Bu kısım şifreleme işlemi için sonra güncellenecek
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.User));
        }
    }
}
