using MediatR;
using PhotoGallery.Data;
using PhotoGallery.Model.DTO;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.PhotoFeatures.Queries
{
    public class GetPhotosQuery : IRequest<IQueryable<PhotoDto>>
    {
        public class GetAllPhotosQueryHandler : IRequestHandler<GetPhotosQuery, IQueryable<PhotoDto>>
        {
            private readonly IPhotoGalleryContext _context;
            public GetAllPhotosQueryHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public Task<IQueryable<PhotoDto>> Handle(GetPhotosQuery request, CancellationToken cancellationToken)
            {
                IQueryable<PhotoDto> photoListQuery = _context.Photos
                    .OrderBy(p => p.AddDate)
                    .Select(p => new PhotoDto
                    {
                        Id = p.Id,
                        FileName = p.FileName,
                        AddDate = p.AddDate,
                        Description = p.Description,
                        AlbumId = p.AlbumId,
                        Tags = String.Join(";", p.Tags.Select(t => t.Name).ToArray())
                    });

                return Task.FromResult(photoListQuery);
            }
        }
    }
}
