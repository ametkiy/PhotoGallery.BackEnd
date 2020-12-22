using MediatR;
using Newtonsoft.Json;
using PhotoGalary.Data;
using PhotoGalary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.PhotoFeatures.Commands
{
    public class CreatePhotoCommand : IRequest<int>
    {
        public string FileName { get; set; }

        public string Description { get; set; }

        public int? AlbumId { get; set; }

        public byte[] PhotoData { get; set; }
        public DateTime AddDate { get; set; }

        public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, int>
        {
            private readonly IPhotoGalaryContext _context;
            public CreatePhotoCommandHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreatePhotoCommand command, CancellationToken cancellationToken)
            {
                var photo = new Photo();
                photo.FileName = command.FileName;
                photo.Description = command.Description;
                //photo.AlbumId = command.AlbumId;
                photo.PhotoData = command.PhotoData;
                photo.AddDate = command.AddDate;
                _context.Photos.Add(photo);
                await _context.SaveChanges();
                return photo.Id;
            }
        }
    }
}
