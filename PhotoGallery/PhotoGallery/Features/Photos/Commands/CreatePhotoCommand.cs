using MediatR;
using Microsoft.AspNetCore.Http;
using PhotoGallery.Data;
using PhotoGallery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using PhotoGallery.Exceptions;

namespace PhotoGallery.Features.PhotoFeatures.Commands
{
    public class CreatePhotoCommand : IRequest<IEnumerable<Guid>>
    {
        public IFormFileCollection FormFiles;
        public Guid? AlbumId { get; set; }
        public String UserId { get; set; }

        public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, IEnumerable<Guid>>
        {
            private readonly IPhotoGalleryContext _context;
            public CreatePhotoCommandHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Guid>> Handle(CreatePhotoCommand command, CancellationToken cancellationToken)
            {
                List<Photo> photos = new List<Photo>();

                foreach (var file in command.FormFiles)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        var fileMimeType = CheckMimeType.GetMimeType(fileBytes, file.FileName);

                        var photo = new Photo();
                        photo.FileName = file.FileName;
                        photo.PhotoData = fileBytes;
                        photo.AddDate = DateTime.Now;
                        photo.FileMimeType = fileMimeType;
                        photo.ApplicationUserId = command.UserId;
                        if (command.AlbumId != Guid.Empty)
                            photo.AlbumId = command.AlbumId;

                        photos.Add(photo);
                    }
                }

                if (photos.Count > 0)
                {
                    _context.Photos.AddRange(photos);
                    await _context.SaveChangesAsync(cancellationToken);

                    return photos.Select(p => p.Id).ToList();
                }

                return null;
            }
        }


    }

}
