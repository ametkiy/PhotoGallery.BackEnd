using AutoMapper;
using MediatR;
using PhotoGalary.Data;
using PhotoGalary.Model;
using PhotoGallery.Exceptions;
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

        public class CreateAlbumComandHandler : IRequestHandler<CreateAlbumCommand, Guid>
        {
            private readonly IPhotoGalleryContext _context;
            public CreateAlbumComandHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(CreateAlbumCommand command, CancellationToken cancellationToken)
            {

                var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateAlbumCommand, Album>());
                var mapper = new Mapper(config);
                Album album = mapper.Map<CreateAlbumCommand, Album>(command);

                if(String.IsNullOrEmpty(album.Title) || String.IsNullOrWhiteSpace(album.Title))
                {
                    throw new FieldIsEmptyException("Album title must be completed");
                }
                _context.Albums.Add(album);
                await _context.SaveChangesAsync(cancellationToken);
                return album.Id;
            }
        }
    }
}
