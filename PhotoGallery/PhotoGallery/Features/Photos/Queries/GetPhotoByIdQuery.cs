using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Model;
using PhotoGallery.Exceptions;
using PhotoGallery.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace PhotoGallery.Features.PhotoFeatures.Queries
{
    public class GetPhotoByIdQuery : IRequest<PhotoDataDto>
    {
        public Guid Id { get; set; }
        public class GetPhotoByIdQueryHandler : IRequestHandler<GetPhotoByIdQuery, PhotoDataDto>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IMapper _mapper;
            public GetPhotoByIdQueryHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<PhotoDataDto> Handle(GetPhotoByIdQuery query, CancellationToken cancellationToken)
            {
                var photoDto = await _context.Photos.AsNoTracking()
                                     .Where(p=> p.Id == query.Id)
                                     .ProjectTo<PhotoDataDto>(_mapper.ConfigurationProvider)
                                     .FirstOrDefaultAsync();

                if (photoDto == null)
                    throw new PhotoNotFoundException(query.Id);

                return photoDto;
            }
        }
    }
}
