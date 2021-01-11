using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Model;
using PhotoGallery.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.AlbumFeatures.Queries
{
    public class GetAllAlbumsQuery : IRequest<IEnumerable<AlbumDto>>
    {
        public class GetAllAlbumsQueryHandler : IRequestHandler<GetAllAlbumsQuery, IEnumerable<AlbumDto>>
        {
            private readonly IPhotoGalleryContext _context;
            public GetAllAlbumsQueryHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<AlbumDto>> Handle(GetAllAlbumsQuery request, CancellationToken cancellationToken)
            {
                var albumList = await _context.Albums
                    .Select(a => new AlbumDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Description = a.Description,
                        Tags = String.Join(";", a.Tags.Select(t => t.Name).ToArray())
                    })
                    .OrderBy(p =>p.Title)
                    .ToListAsync();
                return albumList.AsReadOnly();
            }
        }
    }
}
