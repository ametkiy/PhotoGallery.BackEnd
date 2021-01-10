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

namespace PhotoGallery.Features.PhotoFeatures.Queries
{
    public class GetPaginationPhotosByTagQuery : IRequest<IQueryable<PhotoDto>>
    {
        public string Tag { get; set; }

        public class GetPhotoByTagQueryHandler : IRequestHandler<GetPaginationPhotosByTagQuery, IQueryable<PhotoDto>>
        {
            private readonly IPhotoGalleryContext _context;
            public GetPhotoByTagQueryHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }

            public async Task<IQueryable<PhotoDto>> Handle(GetPaginationPhotosByTagQuery request, CancellationToken cancellationToken)
            {
                IQueryable<PhotoDto> resultQuery;

                var photosWithTagQuery = from p in _context.Photos
                                         where (p.Tags.Any(t => EF.Functions.Like(t.Name, request.Tag)))
                                         select p;

                var albumTagPhotosQuery = from p in _context.Photos
                                          where p.AlbumId != null && (_context.Albums.Where(a => a.Tags.Any(at => EF.Functions.Like(at.Name, request.Tag))).Select(a => a.Id)).Contains((Guid)p.AlbumId)
                                          select p;

                resultQuery =  (photosWithTagQuery.Union(albumTagPhotosQuery)).Select(p => new PhotoDto
                {
                    Id = p.Id,
                    FileName = p.FileName,
                    AddDate = p.AddDate,
                    Description = p.Description,
                    AlbumId = p.AlbumId,
                    Tags = String.Join(";", p.Tags.Select(t => t.Name).ToArray())
                });

                return await Task.FromResult(resultQuery);
            }
        }
    }
}
