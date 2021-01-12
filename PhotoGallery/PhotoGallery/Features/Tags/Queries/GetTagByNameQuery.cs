using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Model.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.Tags.Query
{
    public class GetTagByNameQuery: IRequest<IEnumerable<TagDto>>
    {
        public string Name { get; set; }

        public class GetTagByNameQueryHandler : IRequestHandler<GetTagByNameQuery, IEnumerable<TagDto>>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IMapper _mapper;
            public GetTagByNameQueryHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<IEnumerable<TagDto>> Handle(GetTagByNameQuery request, CancellationToken cancellationToken)
            {
                var tagsDto = await _context.Tags.AsNoTracking()
                     .Where(t => t.Name.Contains(request.Name))
                     .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                     .ToListAsync();

                return tagsDto;
            }
        }
    }
}
