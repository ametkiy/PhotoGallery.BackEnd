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
    public class GetPhotosQuery : IRequest<IQueryable<PhotoDto>>
    {
        public class GetAllPhotosQueryHandler : IRequestHandler<GetPhotosQuery, IQueryable<PhotoDto>>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IMapper _mapper;
            public GetAllPhotosQueryHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public Task<IQueryable<PhotoDto>> Handle(GetPhotosQuery request, CancellationToken cancellationToken)
            {
                IQueryable<PhotoDto> photoListQuery = _context.Photos.AsNoTracking()
                    .OrderBy(p => p.AddDate)
                    .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider);

                return Task.FromResult(photoListQuery);
            }
        }
    }
}
