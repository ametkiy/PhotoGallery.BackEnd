using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGalary.Data;

using PhotoGallery.Model.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.PhotoFeatures.Queries
{
    public class GetAllPhotosQuery : IRequest<IEnumerable<PhotoDto>>
    {
        public class GetAllPhotosQueryHandler : IRequestHandler<GetAllPhotosQuery, IEnumerable<PhotoDto>>
        {
            private readonly IPhotoGalaryContext _context;
            public GetAllPhotosQueryHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<PhotoDto>> Handle(GetAllPhotosQuery request, CancellationToken cancellationToken)
            {
                var photoList = await _context.Photos
                    .Select(p => new PhotoDto
                    {
                        Id = p.Id,
                        FileName = p.FileName,
                        AddDate = p.AddDate
                    })
                    .OrderBy(p => p.AddDate)
                    .ToListAsync();

                return photoList;
                
            }
        }
    }
}
