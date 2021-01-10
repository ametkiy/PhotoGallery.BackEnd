using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGalary.Data;
using PhotoGallery.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.PhotoFeatures.Queries
{
    public class GetPaginationPhotosInAlbumsQuery : IRequest<IQueryable<PhotoDto>>
    {
        public Guid? AlbumId { get; set; }
        public class GetPaginationPhotosInAlbumsQueryHandler : IRequestHandler<GetPaginationPhotosInAlbumsQuery, IQueryable<PhotoDto>>
        {
            private readonly IPhotoGalleryContext _context;
            public GetPaginationPhotosInAlbumsQueryHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public Task<IQueryable<PhotoDto>> Handle(GetPaginationPhotosInAlbumsQuery request, CancellationToken cancellationToken)
            {
                IQueryable<PhotoDto> photoListQuery;
                if (request.AlbumId == Guid.Empty)
                    photoListQuery = _context.Photos
                    .Select(p => new PhotoDto
                    {
                        Id = p.Id,
                        FileName = p.FileName,
                        AddDate = p.AddDate,
                        Description = p.Description,
                        AlbumId = p.AlbumId
                    })
                    .OrderBy(p => p.AddDate).Where(p => !p.AlbumId.HasValue);
                else
                    photoListQuery = _context.Photos
                    .Select(p => new PhotoDto
                    {
                        Id = p.Id,
                        FileName = p.FileName,
                        AddDate = p.AddDate,
                        Description = p.Description,
                        AlbumId = p.AlbumId,
                        Tags = String.Join(";", p.Tags.Select(t => t.Name).ToArray())
                    })
                    .OrderBy(p => p.AddDate).Where(p => p.AlbumId == request.AlbumId);
                return Task.FromResult(photoListQuery);

            }
        }
    }
}
