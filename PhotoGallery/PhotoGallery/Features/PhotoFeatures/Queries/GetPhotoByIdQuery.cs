using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGalary.Data;
using PhotoGalary.Model;
using PhotoGallery.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.PhotoFeatures.Queries
{
    public class GetPhotoByIdQuery : IRequest<PhotoDto>
    {
        public Guid Id { get; set; }
        public class GetPhotoByIdQueryHandler : IRequestHandler<GetPhotoByIdQuery, PhotoDto>
        {
            private readonly IPhotoGalleryContext _context;
            public GetPhotoByIdQueryHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public async Task<PhotoDto> Handle(GetPhotoByIdQuery query, CancellationToken cancellationToken)
            {
                var photoDto = await (from p in _context.Photos
                                        where p.Id == query.Id
                                        select new PhotoDto { 
                                            Id = p.Id,
                                            FileName = p.FileName,
                                            AlbumId = p.AlbumId,
                                            AddDate = p.AddDate,
                                            Description = p.Description,
                                            PhotoData = p.PhotoData,
                                            Tags = String.Join(";", p.Tags.Select(t=>t.Name).ToArray())
                                        }).FirstOrDefaultAsync();
                //var photoDto2 =  await _context.Photos.Select(p => new PhotoDto
                //{
                //    Id = p.Id,
                //    FileName = p.FileName,
                //    AlbumId = p.AlbumId,
                //    AddDate = p.AddDate,
                //    Description = p.Description,
                //    PhotoData = p.PhotoData
                //}).FirstOrDefaultAsync(a => a.Id == query.Id);
                return photoDto;
            }
        }
    }
}
