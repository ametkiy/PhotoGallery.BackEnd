using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Model.DTO;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.PhotoFeatures.Queries
{
    public class GetPhotosInAlbumsQuery : IRequest<IQueryable<PhotoDto>>
    {
        public Guid? AlbumId { get; set; }
        public class GetPaginationPhotosInAlbumsQueryHandler : IRequestHandler<GetPhotosInAlbumsQuery, IQueryable<PhotoDto>>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IMapper _mapper;
            public GetPaginationPhotosInAlbumsQueryHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public Task<IQueryable<PhotoDto>> Handle(GetPhotosInAlbumsQuery request, CancellationToken cancellationToken)
            {
                IQueryable<PhotoDto> photoListQuery;
                if (request.AlbumId == Guid.Empty)
                    photoListQuery = _context.Photos.AsNoTracking()
                        .OrderBy(p => p.AddDate)
                        .Where(p => !p.AlbumId.HasValue)
                        .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider);
                else
                    photoListQuery = _context.Photos.AsNoTracking()
                        .OrderBy(p => p.AddDate)
                        .Where(p => p.AlbumId == request.AlbumId)
                        .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider);

                return Task.FromResult(photoListQuery);

            }
        }
    }
}
