using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGalary.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.PhotoFeatures.Commands
{
    public class DeletePhotoByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeletePhotoByIdCommandHandler : IRequestHandler<DeletePhotoByIdCommand, int>
        {
            private readonly IPhotoGalaryContext _context;
            public DeletePhotoByIdCommandHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeletePhotoByIdCommand command, CancellationToken cancellationToken)
            {
                var photo = await _context.Photos.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (photo == null) return default;
                _context.Photos.Remove(photo);
                await _context.SaveChanges();
                return photo.Id;
            }
        }
    }
}
