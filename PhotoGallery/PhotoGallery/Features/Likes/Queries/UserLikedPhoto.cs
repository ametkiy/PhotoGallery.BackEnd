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
    public class UserLikedPhoto : IRequest<bool>
    {
        public Guid PhotoId { get; set; }
        public string UserId { get; set; }

        public class UserLikedPhotoHandler : IRequestHandler<UserLikedPhoto, bool>
        {
            private IPhotoGalleryContext _context;
            public UserLikedPhotoHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(UserLikedPhoto request, CancellationToken cancellationToken)
            {
                
                var result = await _context.Likes
                    .AnyAsync(l => l.Photo.Id.Equals(request.PhotoId) && l.User.Id.Equals(request.UserId));
                return result;
            }
        }
    }
}
