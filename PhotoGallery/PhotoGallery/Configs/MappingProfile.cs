using AutoMapper;
using PhotoGallery.Features.AlbumFeatures.Commands;
using PhotoGallery.Features.PhotoFeatures.Commands;
using PhotoGallery.Features.Tags.Commands;
using PhotoGallery.Model;
using PhotoGallery.Model.DTO;
using PhotoGallery.Model.Entities;

namespace PhotoGallery.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAlbumCommand, Album>().ForMember(c => c.Tags, act => act.Ignore());
            CreateMap<UpdateAlbumCommand, Album>().ForMember(c => c.Tags, act => act.Ignore());

            CreateMap<Album, AlbumDto>();
            CreateMap<AlbumDto, Album>();

            CreateMap<ApplicationUser, UserInfoDto>();

            CreateMap<Photo, PhotoDto>()
                .ForMember(p => p.FirstName, s => s.MapFrom(v => v.ApplicationUser.FirstName))
                .ForMember(p => p.UserId, s => s.MapFrom(v => v.ApplicationUser.Id))
                .ForMember(p => p.LastName, s => s.MapFrom(v => v.ApplicationUser.LastName))
                .ForMember(p => p.LikesCount, s => s.MapFrom(v => v.Likes.Count));
            CreateMap<PhotoDto, Photo>();
            CreateMap<UpdatePhotoCommand, Photo>().ForMember(c => c.Tags, act => act.Ignore());

            CreateMap<Photo, PhotoDataDto>();
            CreateMap<Photo, PhotoFileDto>();

            CreateMap<Tag, TagDto>();
            CreateMap<TagDto, Tag>();

            CreateMap<CreateTagCommand, Tag>();

            CreateMap<Like, UserLikedPhotoDto>()
                 .ForMember(u => u.FullName, l => l.MapFrom(a => a.User.FirstName + " " + a.User.LastName));

        }
    }
}
