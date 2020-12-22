using MediatR;
using PhotoGalary.Data;
using PhotoGalary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.AlbumFeatures.Queries
{
    public class GetAlbumByIdQuery : IRequest<Album>
    {
        public int Id { get; set; }

        public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, Album>
        {
            private readonly IPhotoGalaryContext _context;
            public GetAlbumByIdQueryHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<Album> Handle(GetAlbumByIdQuery query, CancellationToken cancellationToken)
            {
                var album = _context.Albums.Where(a => a.Id == query.Id).FirstOrDefault();
                if (album == null) return null;
                return album;
            }
        }
    }
}
