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

            CreateMap<Photo, PhotoDto>(); 
            CreateMap<PhotoDto, Photo>();
            CreateMap<UpdatePhotoCommand, Photo>().ForMember(c => c.Tags, act => act.Ignore());

            CreateMap<Photo, PhotoDataDto>();

            CreateMap<Tag, TagDto>();
            CreateMap<TagDto, Tag>();

            CreateMap<CreateTagCommand, Tag>();

        }
    }
}
