using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Exceptions;
using PhotoGallery.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.AlbumFeatures.Commands
{
    public class UpdateAlbumCommand : IRequest<Guid>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Guid>
        {
            private readonly IPhotoGalleryContext _context;
            public UpdateAlbumCommandHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(UpdateAlbumCommand command, CancellationToken cancellationToken)
            {
                if (String.IsNullOrEmpty(command.Title) || String.IsNullOrWhiteSpace(command.Title))
                {
                    throw new FieldIsEmptyException("Album title must be completed");
                }

                var album = _context.Albums.Include(t => t.Tags).FirstOrDefault(a => a.Id == command.Id);

                if (album == null)
                {
                    throw new AlbumNotFoundException(command.Id);
                }
                else
                {
                    album.Title = command.Title;
                    album.Description = command.Description;

                    album.Tags.Clear();
                    foreach (var tag in command.Tags)
                    {
                        Tag tmp = _context.Tags.FirstOrDefault(t => t.Id==tag.Id);
                        if(tmp!=null)
                            album.Tags.Add(tmp);
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                    return album.Id;
                }
            }
        }
    }
}
