using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Exceptions;
using PhotoGallery.Model.DTO;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.Photos.Queries
{
    public class GetPhotoFileByIdQuery : IRequest<PhotoFileDto>
    {
        public Guid Id { get; set; }

        public class GetPhotoFileByIdQueryHandler : IRequestHandler<GetPhotoFileByIdQuery, PhotoFileDto>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IMapper _mapper;
            public GetPhotoFileByIdQueryHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<PhotoFileDto> Handle(GetPhotoFileByIdQuery query, CancellationToken cancellationToken)
            {
                var photoData = await _context.Photos.AsNoTracking()
                                     .Where(p => p.Id == query.Id)
                                     .ProjectTo<PhotoFileDto>(_mapper.ConfigurationProvider)
                                     .FirstOrDefaultAsync();

                if (photoData == null)
                    throw new PhotoNotFoundException(query.Id);

                return photoData;
            }
        }

    }
}
