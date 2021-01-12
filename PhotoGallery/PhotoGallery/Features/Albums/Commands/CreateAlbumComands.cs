﻿using AutoMapper;
using MediatR;
using PhotoGallery.Data;
using PhotoGallery.Model;
using PhotoGallery.Exceptions;
using PhotoGallery.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.AlbumFeatures.Commands
{
    public class CreateAlbumCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public class CreateAlbumComandHandler : IRequestHandler<CreateAlbumCommand, Guid>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IMapper _mapper;
            public CreateAlbumComandHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Guid> Handle(CreateAlbumCommand command, CancellationToken cancellationToken)
            {
                Album album = _mapper.Map<CreateAlbumCommand, Album>(command);

                if(String.IsNullOrWhiteSpace(album.Title))
                {
                    throw new FieldIsEmptyException("Album title must be completed");
                }

                foreach (var tag in command.Tags)
                {
                    Tag tmp = _context.Tags.FirstOrDefault(t => t.Id == tag.Id);
                    if (tmp != null)
                        album.Tags.Add(tmp);
                }

                _context.Albums.Add(album);
                await _context.SaveChangesAsync(cancellationToken);
                return album.Id;
            }
        }
    }
}
