using AutoMapper;
using MediatR;
using PhotoGallery.Data;
using PhotoGallery.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.Photos.Commands
{
    public class SetLike : IRequest<bool>
    {
        public string UserId { get; set; }
        public Guid PhotoId { get; set; }

        public class SetLikeHandler : IRequestHandler<SetLike, bool>
        {
            private readonly IPhotoGalleryContext _context;

            public SetLikeHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(SetLike request, CancellationToken cancellationToken)
            {
                var tmp = _context.Likes.Where(l => l.Photo.Id.Equals(request.PhotoId) && l.User.Id.Equals(request.UserId)).FirstOrDefault();
                if (tmp != null)
                {
                    _context.Likes.Remove(tmp);
                    await _context.SaveChangesAsync(cancellationToken);
                    return false;
                }
                else
                {
                    var photo = _context.Photos.FirstOrDefault(p => p.Id.Equals(request.PhotoId));
                    var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id.Equals(request.UserId));
                    if (photo != null && user != null)
                    {
                        _context.Likes.Add(new Like { Photo = photo, User = user });
                        await _context.SaveChangesAsync(cancellationToken);
                        return true;
                    }
                }

                return false;                 
            }
        }
    }
}
