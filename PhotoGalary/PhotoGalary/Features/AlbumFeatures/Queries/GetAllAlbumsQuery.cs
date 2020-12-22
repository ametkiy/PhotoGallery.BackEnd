using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGalary.Data;
using PhotoGalary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.AlbumFeatures.Queries
{
    public class GetAllAlbumsQuery : IRequest<IEnumerable<Album>>
    {
        public class GetAllAlbumsQueryHandler : IRequestHandler<GetAllAlbumsQuery, IEnumerable<Album>>
        {
            private readonly IPhotoGalaryContext _context;
            public GetAllAlbumsQueryHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Album>> Handle(GetAllAlbumsQuery request, CancellationToken cancellationToken)
            {
                var albumList = await _context.Albums.ToListAsync();
                if (albumList == null)
                    return null;
                else
                    return albumList.AsReadOnly();
            }
        }
    }
}
