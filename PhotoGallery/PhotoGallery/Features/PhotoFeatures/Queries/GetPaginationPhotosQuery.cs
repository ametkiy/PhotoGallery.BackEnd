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
    public class GetPaginationPhotosQuery : IRequest<PageOfPhotoDto>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }

        public class GetAllPhotosQueryHandler : IRequestHandler<GetPaginationPhotosQuery, PageOfPhotoDto>
        {
            private readonly IPhotoGalaryContext _context;
            public GetAllPhotosQueryHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<PageOfPhotoDto> Handle(GetPaginationPhotosQuery request, CancellationToken cancellationToken)
            {
                var pageOfPhoto = new PageOfPhotoDto();
                var photoList = await _context.Photos
                    .Select(p => new PhotoDto
                    {
                        Id = p.Id,
                        FileName = p.FileName,
                        AddDate = p.AddDate
                    })
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .OrderBy(p => p.AddDate)
                    .ToListAsync();
                if (photoList == null)
                    return null;
                else
                {
                    pageOfPhoto.Photos = photoList.ToArray();
                    pageOfPhoto.Count = await _context.Photos.CountAsync();
                    return pageOfPhoto;
                }
            }
        }
    }
}
