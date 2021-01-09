using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGalary.Data;
using PhotoGallery.Exceptions;
using PhotoGallery.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.AlbumFeatures.Commands
{
    public class UpdateAlbumCommand : IRequest<Guid>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
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
                        if (!String.IsNullOrWhiteSpace(tag))
                        {
                            var tmp = _context.Tags.FirstOrDefault(t => t.Name == tag);
                            if (tmp != null)
                            {   if (!album.Tags.Contains(tmp))
                                    album.Tags.Add(tmp);
                            }
                            else
                            {
                                Tag tmpTag = new Tag { Name = tag };
                                _context.Tags.Add(tmpTag);
                                album.Tags.Add(tmpTag);
                            }
                        }
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                    return album.Id;
                }
            }
        }
    }
}
