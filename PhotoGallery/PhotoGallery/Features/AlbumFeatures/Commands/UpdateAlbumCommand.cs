using MediatR;
using PhotoGalary.Data;
using PhotoGallery.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.AlbumFeatures.Commands
{
    public class UpdateAlbumCommand : IRequest<Guid>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Guid>
        {
            private readonly IPhotoGalaryContext _context;
            public UpdateAlbumCommandHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(UpdateAlbumCommand command, CancellationToken cancellationToken)
            {
                if (String.IsNullOrEmpty(command.Title) || String.IsNullOrWhiteSpace(command.Title))
                {
                    throw new FieldIsEmptyException("Album title must be completed");
                }

                var album = _context.Albums.FirstOrDefault(a => a.Id == command.Id);

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
