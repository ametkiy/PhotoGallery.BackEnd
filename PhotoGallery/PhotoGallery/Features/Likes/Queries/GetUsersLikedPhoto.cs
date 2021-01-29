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

namespace PhotoGallery.Features.Likes.Queries
{
    public class GetUsersLikedPhoto : IRequest<IEnumerable<UserLikedPhotoDto>>
    {
        public Guid PhotoId { get; set; }

        public class GetUsersWhoLikePhotoHandler : IRequestHandler<GetUsersLikedPhoto, IEnumerable<UserLikedPhotoDto>>
        {
            IPhotoGalleryContext _context;
            private readonly IMapper _mapper;
            public GetUsersWhoLikePhotoHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<IEnumerable<UserLikedPhotoDto>> Handle(GetUsersLikedPhoto request, CancellationToken cancellationToken)
            {
                var result = await _context.Likes
                    .Include(p => p.Photo)
                    .Include(p => p.User)
                    .Where(p => p.Photo.Id.Equals(request.PhotoId))
                    .ProjectTo<UserLikedPhotoDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken: cancellationToken);


                return result;
            }
        }
    }
}
