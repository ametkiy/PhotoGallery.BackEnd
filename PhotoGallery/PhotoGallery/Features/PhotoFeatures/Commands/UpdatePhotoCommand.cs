using MediatR;
using PhotoGalary.Data;
using PhotoGallery.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.PhotoFeatures.Commands
{
    public class UpdatePhotoCommand : IRequest<Guid>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid? AlbumId { get; set; }
        public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand, Guid>
        {
            private readonly IPhotoGalaryContext _context;
            public UpdatePhotoCommandHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(UpdatePhotoCommand command, CancellationToken cancellationToken)
            {
                var photo = _context.Photos.FirstOrDefault(a => a.Id == command.Id);

                if (photo == null)
                {
                    throw new PhotoNotFoundException(command.Id);
                }
                else
                {
                    photo.Description = command.Description;
                    photo.AlbumId = command.AlbumId;
                    await _context.SaveChangesAsync(cancellationToken);
                    return photo.Id;
                }
            }
        }
    }
}
