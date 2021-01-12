using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Exceptions;
using PhotoGallery.Model.DTO;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace PhotoGallery.Features.AlbumFeatures.Queries
{
    public class GetAlbumByIdQuery : IRequest<AlbumDto>
    {
        public Guid Id { get; set; }

        public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, AlbumDto>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IMapper _mapper;
            public GetAlbumByIdQueryHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<AlbumDto> Handle(GetAlbumByIdQuery query, CancellationToken cancellationToken)
            {
                var albumDto = await _context.Albums.AsNoTracking()
                     .ProjectTo<AlbumDto>(_mapper.ConfigurationProvider)
                     .FirstOrDefaultAsync(a => a.Id == query.Id);

                if (albumDto == null)
                    throw new AlbumNotFoundException(query.Id);

                return albumDto;
            }
        }
    }
}
