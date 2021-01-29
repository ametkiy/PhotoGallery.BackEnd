using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.Likes.Queries
{
    public class GetPhotoLikesCount :IRequest<int>
    {
        public Guid PhotoId { get; set; }

        public class GetPhotoLikesCountHandler : IRequestHandler<GetPhotoLikesCount, int>
        {
            IPhotoGalleryContext _context;
            public GetPhotoLikesCountHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(GetPhotoLikesCount request, CancellationToken cancellationToken)
            {
                var result = await _context.Photos.AsNoTracking()
                    .Include(p => p.Likes)
                    .Where(p => p.Id.Equals(request.PhotoId))
                    .Select(p => p.Likes.Count()).SingleOrDefaultAsync(cancellationToken);

                return result;
            }
        }
    }
}
