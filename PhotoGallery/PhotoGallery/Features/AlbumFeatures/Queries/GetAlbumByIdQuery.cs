using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGalary.Data;
using PhotoGalary.Model;
using PhotoGallery.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.AlbumFeatures.Queries
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
                var album = await _context.Albums
                    .Select(a => new AlbumDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Description = a.Description,
                    }).FirstOrDefaultAsync(a => a.Id == query.Id);

                return album;
            }
        }
    }
}
