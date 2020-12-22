using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGalary.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.AlbumFeatures.Commands
{
    public class DeleteAlbumByIdCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public class DeleteAlbumByIdCommandHandler : IRequestHandler<DeleteAlbumByIdCommand, Guid>
        {
            private readonly IPhotoGalaryContext _context;
            public DeleteAlbumByIdCommandHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(DeleteAlbumByIdCommand command, CancellationToken cancellationToken)
            {
                var album = await _context.Albums.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (album == null) return default;
                _context.Albums.Remove(album);
                await _context.SaveChangesAsync(cancellationToken);
                return album.Id;
            }
        }
    }
}
