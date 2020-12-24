using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PhotoGalary.Data;
using PhotoGalary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PhotoGallery;
using System.IO;
using Microsoft.Extensions.Configuration;
using PhotoGallery.Exceptions;

namespace PhotoGalary.Features.PhotoFeatures.Commands
{
    public class CreatePhotoCommand : IRequest<CreatePhotoResult>
    {
        public IFormFileCollection FormFiles;

        public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, CreatePhotoResult>
        {
            private readonly IPhotoGalaryContext _context;
            private readonly IConfiguration _config;
            public CreatePhotoCommandHandler(IPhotoGalaryContext context, IConfiguration config)
            {
                _context = context;
                _config = config;
            }

            public async Task<CreatePhotoResult> Handle(CreatePhotoCommand command, CancellationToken cancellationToken)
            {
                CreatePhotoResult createPhotoResult = new CreatePhotoResult();
                List<Photo> photos = new List<Photo>();
                var fileSizeLimit = _config.GetValue<long>("FileSizeLimit");

                foreach (var file in command.FormFiles)
                {
                    try
                    {
                        if (file.Length == 0)
                            throw new FileSizeException($"File {file.FileName} has no content.");

                        if (file.Length > fileSizeLimit)
                            throw new FileSizeException(file.FileName, file.Length, fileSizeLimit);

                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();

                            if (CheckMimeType.GetMimeType(fileBytes, file.FileName) == CheckMimeType.DefaultMimeTipe)
                            {
                                throw new UnsupportedFileFormatException(file.FileName);
                            }
                            else
                            {
                                var photo = new Photo();
                                photo.FileName = file.FileName;
                                photo.PhotoData = fileBytes;
                                photo.Description = "";
                                photo.AddDate = DateTime.Now;

                                photos.Add(photo);
                            }
                        }
                    }
                    catch (UnsupportedFileFormatException ex)
                    {
                        createPhotoResult.errors.Add(ex.Message);
                    }
                    catch (FileSizeException ex)
                    {
                        createPhotoResult.errors.Add(ex.Message);
                    }
                }

                if (photos.Count > 0)
                {
                    _context.Photos.AddRange(photos);
                    await _context.SaveChangesAsync(cancellationToken);

                    createPhotoResult.guids = photos.Select(p => p.Id).ToList();
                }

                return createPhotoResult;
            }
        }


    }
    public class CreatePhotoResult
    {
        public List<String> errors { get; set; } = new List<string>();
        public List<Guid> guids { get; set; } = new List<Guid>();
    }
}
