using MediatR;
using PhotoGalary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.PhotoFeatures.Commands
{
    public class UpdatePhotoCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? AlbumId { get; set; }
        public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand, int>
        {
            private readonly IPhotoGalaryContext _context;
            public UpdatePhotoCommandHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdatePhotoCommand command, CancellationToken cancellationToken)
            {
                var photo = _context.Photos.Where(a => a.Id == command.Id).FirstOrDefault();

                if (photo == null)
                {
                    return default;
                }
                else
                {
                    photo.Description = command.Description;
                    //photo.AlbumId = command.AlbumId;
                    await _context.SaveChangesAsync(cancellationToken);
                    return photo.Id;
                }
            }
        }
    }
}
