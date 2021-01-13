﻿using MediatR;
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

        public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, IEnumerable<Guid>>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IConfiguration _config;
            public CreatePhotoCommandHandler(IPhotoGalleryContext context, IConfiguration config)
            {
                _context = context;
                _config = config;
            }

            public async Task<IEnumerable<Guid>> Handle(CreatePhotoCommand command, CancellationToken cancellationToken)
            {
                if (command.FormFiles.Count == 0)
                    throw new FieldIsEmptyException("No file data");
                List<Photo> photos = new List<Photo>();
                var fileSizeLimit = _config.GetValue<long>("FileSizeLimit");

                foreach (var file in command.FormFiles)
                {
                    if (file.Length == 0)
                        throw new FileSizeException($"File {file.FileName} has no content.");

                    if (fileSizeLimit > 0 && file.Length > fileSizeLimit)
                        throw new FileSizeException(file.FileName, file.Length, fileSizeLimit);

                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        var fileMimeType = CheckMimeType.GetMimeType(fileBytes, file.FileName);

                        if (fileMimeType == CheckMimeType.DefaultMimeTipe)
                        {
                            throw new UnsupportedFileFormatException(file.FileName);
                        }
                        else
                        {
                            var photo = new Photo();
                            photo.FileName = file.FileName;
                            photo.PhotoData = fileBytes;
                            photo.AddDate = DateTime.Now;
                            photo.FileMimeType = fileMimeType;
                            if (command.AlbumId != Guid.Empty)
                                photo.AlbumId = command.AlbumId;

                            photos.Add(photo);
                        }
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
