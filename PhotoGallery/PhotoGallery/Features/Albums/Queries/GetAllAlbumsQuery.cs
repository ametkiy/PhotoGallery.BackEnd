using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            private readonly IMapper _mapper;

            public GetAllAlbumsQueryHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<IEnumerable<AlbumDto>> Handle(GetAllAlbumsQuery request, CancellationToken cancellationToken)
            {
                var albumList = await _context.Albums.AsNoTracking()
                    .OrderBy(p => p.Title)
                    .ProjectTo<AlbumDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return albumList.AsReadOnly();
            }
        }
    }
}
