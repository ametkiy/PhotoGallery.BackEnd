using MediatR;
using Microsoft.EntityFrameworkCore;
using PhotoGalary.Data;
using PhotoGalary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGalary.Features.PhotoFeatures.Queries
{
    public class GetAllPhotosQuery : IRequest<PageOfPhoto>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }

        public class GetAllPhotosQueryHandler : IRequestHandler<GetAllPhotosQuery, PageOfPhoto>
        {
            private readonly IPhotoGalaryContext _context;
            public GetAllPhotosQueryHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<PageOfPhoto> Handle(GetAllPhotosQuery request, CancellationToken cancellationToken)
            {
                PageOfPhoto pageOfPhoto = new PageOfPhoto();
                //var photoList = await _context.Photos.ToListAsync();
                var photoList = await _context.Photos.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                if (photoList == null)
                    return null;
                else
                {

                    pageOfPhoto.Photos = photoList.ToArray();
                    pageOfPhoto.Count = await _context.Photos.CountAsync();
                    pageOfPhoto.PageSize = request.PageSize;
                    pageOfPhoto.Page = request.Page;
                    return pageOfPhoto;
                }
            }
        }
    }

    public class PageOfPhoto
    {
        public Photo[] Photos { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }

    }
}
