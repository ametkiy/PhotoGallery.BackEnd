using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using PhotoGallery.Data;
using PhotoGallery.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.Photos.Queries
{
    public class GetPhotoFileByIdQuery : IRequest<FileContentResult>
    {
        public Guid Id { get; set; }

        public class GetPhotoFileByIdQueryHandler : IRequestHandler<GetPhotoFileByIdQuery, FileContentResult>
        {
            private readonly IPhotoGalleryContext _context;
            public GetPhotoFileByIdQueryHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }
            public async Task<FileContentResult> Handle(GetPhotoFileByIdQuery query, CancellationToken cancellationToken)
            {
                var photoData = await _context.Photos.AsNoTracking()
                                     .Where(p => p.Id == query.Id)
                                     .Select(p => new { p.PhotoData, p.FileMimeType })
                                     .FirstOrDefaultAsync();

                if (photoData == null)
                    throw new PhotoNotFoundException(query.Id);

                FileContentResult fileStreamResult = new FileContentResult(photoData.PhotoData, photoData.FileMimeType);

                return fileStreamResult;
            }
        }

    }
}
