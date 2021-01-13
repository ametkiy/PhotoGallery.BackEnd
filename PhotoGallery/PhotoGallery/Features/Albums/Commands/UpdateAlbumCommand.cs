using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Exceptions;
using PhotoGallery.Model;
using PhotoGallery.Model.Commands;
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
        public List<TagShort> Tags { get; set; }
        public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Guid>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IMapper _mapper;
            public UpdateAlbumCommandHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Guid> Handle(UpdateAlbumCommand command, CancellationToken cancellationToken)
            {
                var album = _context.Albums.Include(t => t.Tags).FirstOrDefault(a => a.Id == command.Id);

                if (album == null)
                {
                    throw new AlbumNotFoundException(command.Id);
                }
                else
                {
                    album = _mapper.Map<UpdateAlbumCommand, Album>(command, album);

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
