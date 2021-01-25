using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.PhotoFeatures.Commands
{
    public class DeletePhotoByIdCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }

        public class DeletePhotoByIdCommandHandler : IRequestHandler<DeletePhotoByIdCommand, Unit>
        {
            private readonly IPhotoGalleryContext _context;
            public DeletePhotoByIdCommandHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeletePhotoByIdCommand command, CancellationToken cancellationToken)
            {
                var photo = await _context.Photos.Where(a => a.Id == command.Id).FirstOrDefaultAsync();

                if (photo == null) 
                    throw new PhotoNotFoundException(command.Id);

                if (photo.ApplicationUserId != command.UserId)
                    throw new NoDeleteRightsException("You have no rights to delete other people's photos");

                _context.Photos.Remove(photo);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
