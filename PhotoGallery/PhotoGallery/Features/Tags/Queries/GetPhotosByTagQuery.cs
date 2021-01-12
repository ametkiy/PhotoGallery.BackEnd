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
    public class GetPhotosByTagQuery : IRequest<IQueryable<PhotoDto>>
    {
        public string Tag { get; set; }

        public class GetPhotoByTagQueryHandler : IRequestHandler<GetPhotosByTagQuery, IQueryable<PhotoDto>>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IMapper _mapper;
            public GetPhotoByTagQueryHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IQueryable<PhotoDto>> Handle(GetPhotosByTagQuery request, CancellationToken cancellationToken)
            {
                IQueryable<PhotoDto> resultQuery;

                var photosWithTagQuery = from p in _context.Photos
                                         where (p.Tags.Any(t => EF.Functions.Like(t.Name, request.Tag)))
                                         select p;

                var albumTagPhotosQuery = from p in _context.Photos
                                          where p.AlbumId != null && (_context.Albums.Where(a => a.Tags.Any(at => EF.Functions.Like(at.Name, request.Tag))).Select(a => a.Id)).Contains((Guid)p.AlbumId)
                                          select p;

                resultQuery =  (photosWithTagQuery.Union(albumTagPhotosQuery)).ProjectTo<PhotoDto>(_mapper.ConfigurationProvider);

                return await Task.FromResult(resultQuery);
            }
        }
    }
}
