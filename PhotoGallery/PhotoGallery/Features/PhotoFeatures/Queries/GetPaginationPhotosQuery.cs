using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGalary.Data;
using PhotoGallery.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.PhotoFeatures.Queries
{
    public class GetPaginationPhotosQuery : IRequest<IQueryable<PhotoDto>>
    {
        public class GetAllPhotosQueryHandler : IRequestHandler<GetPaginationPhotosQuery, IQueryable<PhotoDto>>
        {
            private readonly IPhotoGalaryContext _context;
            public GetAllPhotosQueryHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public Task<IQueryable<PhotoDto>> Handle(GetPaginationPhotosQuery request, CancellationToken cancellationToken)
            {
                IQueryable<PhotoDto> photoListQuery = _context.Photos
                    .Select(p => new PhotoDto
                    {
                        Id = p.Id,
                        FileName = p.FileName,
                        AddDate = p.AddDate
                    })
                    .OrderBy(p => p.AddDate);

                return Task.FromResult(photoListQuery);
            }
        }
    }
}
