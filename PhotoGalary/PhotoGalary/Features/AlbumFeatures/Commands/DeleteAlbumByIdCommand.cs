using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGalary.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.AlbumFeatures.Commands
{
    public class DeleteAlbumByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteAlbumByIdCommandHandler : IRequestHandler<DeleteAlbumByIdCommand, int>
        {
            private readonly IPhotoGalaryContext _context;
            public DeleteAlbumByIdCommandHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteAlbumByIdCommand command, CancellationToken cancellationToken)
            {
                var album = await _context.Albums.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (album == null) return default;
                _context.Albums.Remove(album);
                await _context.SaveChanges();
                return album.Id;
            }
        }
    }
}
