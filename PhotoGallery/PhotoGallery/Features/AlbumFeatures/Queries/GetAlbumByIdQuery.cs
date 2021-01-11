using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Model;
using PhotoGallery.Exceptions;
using PhotoGallery.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.AlbumFeatures.Queries
{
    public class GetAlbumByIdQuery : IRequest<AlbumDto>
    {
        public Guid Id { get; set; }

        public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, AlbumDto>
        {
            private readonly IPhotoGalleryContext _context;
            public GetAlbumByIdQueryHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public async Task<AlbumDto> Handle(GetAlbumByIdQuery query, CancellationToken cancellationToken)
            {
                var albumDto = await _context.Albums
                    .Select(a => new AlbumDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Description = a.Description,
                        Tags = String.Join(";", a.Tags.Select(t => t.Name).ToArray())
                    }).FirstOrDefaultAsync(a => a.Id == query.Id);

                if (albumDto == null)
                    throw new AlbumNotFoundException(query.Id);

                return albumDto;
            }
        }
    }
}
