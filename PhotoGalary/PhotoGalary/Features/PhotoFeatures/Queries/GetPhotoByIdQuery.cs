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
    public class GetPhotoByIdQuery : IRequest<Photo>
    {
        public int Id { get; set; }
        public class GetPhotoByIdQueryHandler : IRequestHandler<GetPhotoByIdQuery, Photo>
        {
            private readonly IPhotoGalaryContext _context;
            public GetPhotoByIdQueryHandler(IPhotoGalaryContext context)
            {
                _context = context;
            }
            public async Task<Photo> Handle(GetPhotoByIdQuery query, CancellationToken cancellationToken)
            {
                var photo =  await _context.Photos.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
                if (photo == null) return null;
                return photo;
            }
        }
    }
}
