using AutoMapper;
using MediatR;
using PhotoGalary.Data;
using PhotoGalary.Model;
using PhotoGallery.Exceptions;
using PhotoGallery.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.AlbumFeatures.Commands
{
    public class CreateAlbumCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }

        public class CreateAlbumComandHandler : IRequestHandler<CreateAlbumCommand, Guid>
        {
            private readonly IPhotoGalleryContext _context;
            public CreateAlbumComandHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(CreateAlbumCommand command, CancellationToken cancellationToken)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateAlbumCommand, Album>().ForMember(c => c.Tags, act => act.Ignore()));
                var mapper = new Mapper(config);
                Album album = mapper.Map<CreateAlbumCommand, Album>(command);

                if(String.IsNullOrEmpty(album.Title) || String.IsNullOrWhiteSpace(album.Title))
                {
                    throw new FieldIsEmptyException("Album title must be completed");
                }

                if (!String.IsNullOrWhiteSpace(command.Tags))
                {
                    var tagsArray = command.Tags.Split(";");
                    foreach (var tag in tagsArray)
                    {
                        if (!String.IsNullOrWhiteSpace(tag))
                        {
                            var tmp = _context.Tags.FirstOrDefault(t => t.Name == tag);
                            if (tmp != null)
                            {
                                if (!album.Tags.Contains(tmp))
                                    album.Tags.Add(tmp);
                            }
                            else
                            {
                                Tag tmpTag = new Tag { Name = tag };
                                _context.Tags.Add(tmpTag);
                                album.Tags.Add(tmpTag);
                            }
                        }
                    }
                }

                _context.Albums.Add(album);
                await _context.SaveChangesAsync(cancellationToken);
                return album.Id;
            }
        }
    }
}
