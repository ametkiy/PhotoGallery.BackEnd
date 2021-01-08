using MediatR;
using PhotoGalary.Data;
using PhotoGallery.Exceptions;
using PhotoGallery.Model.Entities;
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
        public string[] Tags { get; set; }
        public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand, Guid>
        {
            private readonly IPhotoGalleryContext _context;
            public UpdatePhotoCommandHandler(IPhotoGalleryContext context)
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

                    foreach (var tag in command.Tags)
                    {
                        if (!String.IsNullOrWhiteSpace(tag))
                        {
                            var tmp = _context.Tags.FirstOrDefault(t => t.Name == tag);
                            if (tmp != null)
                                photo.Tags.Add(tmp);
                            else
                            {
                                Tag tmpTag = new Tag { Name = tag };
                                _context.Tags.Add(tmpTag);
                                photo.Tags.Add(tmpTag);
                            }
                        }
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                    return photo.Id;
                }
            }
        }
    }
}
