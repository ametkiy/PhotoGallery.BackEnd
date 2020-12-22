using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGalary.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.PhotoFeatures.Commands
{
    public class DeletePhotoByIdCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public class DeletePhotoByIdCommandHandler : IRequestHandler<DeletePhotoByIdCommand, Guid>
        {
            private readonly IPhotoGalaryContext _context;
            public DeletePhotoByIdCommandHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(DeletePhotoByIdCommand command, CancellationToken cancellationToken)
            {
                var photo = await _context.Photos.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (photo == null) return default;
                _context.Photos.Remove(photo);
                await _context.SaveChangesAsync(cancellationToken);
                return photo.Id;
            }
        }
    }
}
