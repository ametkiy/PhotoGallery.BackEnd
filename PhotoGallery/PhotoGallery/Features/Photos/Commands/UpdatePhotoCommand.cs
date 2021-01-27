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

namespace PhotoGallery.Features.PhotoFeatures.Commands
{
    public class UpdatePhotoCommand : IRequest<Guid>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid? AlbumId { get; set; }
        public List<TagShort> Tags { get; set; } 
        public bool Private { get; set; }
        public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand, Guid>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IMapper _mapper;
            public UpdatePhotoCommandHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Guid> Handle(UpdatePhotoCommand command, CancellationToken cancellationToken)
            {
                var photo = _context.Photos.Include(t=>t.Tags).FirstOrDefault(a => a.Id == command.Id);

                if (photo == null)
                {
                    throw new PhotoNotFoundException(command.Id);
                }
                else
                {
                    _mapper.Map(command, photo);

                    photo.Tags.Clear();
                    foreach (var tag in command.Tags)
                    {
                        Tag tmp = _context.Tags.FirstOrDefault(t => t.Id == tag.Id);
                        if (tmp != null)
                            photo.Tags.Add(tmp);
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                    return photo.Id;
                }
            }
        }
    }
}
