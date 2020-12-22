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

namespace PhotoGalary.Features.PhotoFeatures.Commands
{
    public class CreatePhotoCommand : IRequest<Guid>
    {
        public IFormFileCollection FormFiles;
        //public string FileName { get; set; }

        //public string Description { get; set; }

        //public int? AlbumId { get; set; }

        //public byte[] PhotoData { get; set; }
        //public DateTime AddDate { get; set; }

        public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, Guid>
        {
            private readonly IPhotoGalaryContext _context;
            public CreatePhotoCommandHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(CreatePhotoCommand command, CancellationToken cancellationToken)
            {
                List<Photo> photos = new List<Photo>();
                foreach (var file in command.FormFiles)
                {
                    if (file.Length==0 || file.Length> 3000000)
                        throw new Exception("The file is too large");
                    else 
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            if(CheckMimeType.GetMimeType(fileBytes, file.FileName).StartsWith("image/"))
                            {
                                var photo = new Photo();
                                photo.FileName = file.FileName;
                                photo.PhotoData = fileBytes;
                                photo.Description = "";
                                photo.AddDate = new DateTime();

                                photos.Add(photo);
                            }
                            else
                            {
                                throw new Exception("Unsupported file format");
                            }
                        }
                }

                _context.Photos.AddRange(photos);
                await _context.SaveChangesAsync(cancellationToken);

                return new Guid();
            }
        }
    }
}
