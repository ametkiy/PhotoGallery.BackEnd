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
    public class GetAllAlbumsQuery : IRequest<IEnumerable<AlbumDto>>
    {
        public class GetAllAlbumsQueryHandler : IRequestHandler<GetAllAlbumsQuery, IEnumerable<AlbumDto>>
        {
            private readonly IPhotoGalaryContext _context;
            public GetAllAlbumsQueryHandler(IPhotoGalaryContext context)
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
                        Description = a.Description
                    })
                    .ToListAsync();
                return albumList.AsReadOnly();
            }
        }
    }
}
