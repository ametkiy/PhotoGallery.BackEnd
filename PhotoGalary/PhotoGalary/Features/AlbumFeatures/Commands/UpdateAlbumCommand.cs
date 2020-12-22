using MediatR;
using PhotoGalary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.AlbumFeatures.Commands
{
    public class UpdateAlbumCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, int>
        {
            private readonly IPhotoGalaryContext _context;
            public UpdateAlbumCommandHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateAlbumCommand command, CancellationToken cancellationToken)
            {
                var album = _context.Albums.Where(a => a.Id == command.Id).FirstOrDefault();

                if (album == null)
                {
                    return default;
                }
                else
                {
                    album.Title = command.Title;
                    album.Description = command.Description;
                    await _context.SaveChangesAsync(cancellationToken);
                    return album.Id;
                }
            }
        }
    }
}
