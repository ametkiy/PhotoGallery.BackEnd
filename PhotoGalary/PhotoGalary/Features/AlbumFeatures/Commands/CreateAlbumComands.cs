using MediatR;
using PhotoGalary.Data;
using PhotoGalary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.AlbumFeatures.Commands
{
    public class CreateAlbumCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public class CreateAlbumComandHandler : IRequestHandler<CreateAlbumCommand, int>
        {
            private readonly IPhotoGalaryContext _context;
            public CreateAlbumComandHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateAlbumCommand command, CancellationToken cancellationToken)
            {
                var album = new Album();
                album.Title = command.Title;
                album.Description = command.Description;
                _context.Albums.Add(album);
                await _context.SaveChanges();
                return album.Id;
            }
        }
    }
}
