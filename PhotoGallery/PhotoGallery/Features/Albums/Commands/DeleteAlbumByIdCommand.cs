﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.AlbumFeatures.Commands
{
    public class DeleteAlbumByIdCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public class DeleteAlbumByIdCommandHandler : IRequestHandler<DeleteAlbumByIdCommand, Unit>
        {
            private readonly IPhotoGalleryContext _context;
            public DeleteAlbumByIdCommandHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteAlbumByIdCommand command, CancellationToken cancellationToken)
            {
                var album = await _context.Albums.FirstOrDefaultAsync(a => a.Id == command.Id);

                if (album == null) 
                    throw new AlbumNotFoundException(command.Id);

                _context.Albums.Remove(album);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
