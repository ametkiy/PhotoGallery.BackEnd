using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.Tags.Queries
{
    public class GetTags : IRequest<List<TagDto>>
    {
        public class GetTagsHendler : IRequestHandler<GetTags, List<TagDto>>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IMapper _mapper;

            public GetTagsHendler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<TagDto>> Handle(GetTags request, CancellationToken cancellationToken)
            {
                List<TagDto> tagsList = await _context.Tags.AsNoTracking()
                      .ProjectTo<TagDto>(_mapper.ConfigurationProvider).ToListAsync();

                return tagsList;
            }
        }
    }
}
