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
    public class GetPaginationPhotosByTagQuery : IRequest<IQueryable<PhotoDto>>
    {
        public string Tag { get; set; }

        public class GetPhotoByTagQueryHandler : IRequestHandler<GetPaginationPhotosByTagQuery, IQueryable<PhotoDto>>
        {
            private readonly IPhotoGalleryContext _context;
            public GetPhotoByTagQueryHandler(IPhotoGalleryContext context)
            {
                _context = context;
            }

            public async Task<IQueryable<PhotoDto>> Handle(GetPaginationPhotosByTagQuery request, CancellationToken cancellationToken)
            {
                IQueryable<PhotoDto> photoListQuery;

                var tmp  = _context.Photos.FromSqlRaw("select p.id, p.FileName, p.AlbumId from Photos AS p" +
    "join PhotosTags pt on p.Id = pt.PhotosId" +
    "join Tags t on t.Id = pt.TagsId" +
    "where" +
    "t.Name like 'string'" +
    "UNION" +
    "select p.id, p.FileName, p.AlbumId from Photos AS p" +
     "where" +
     "p.AlbumId in (" +
     "SELECT a.Id from Albums a" +
        "join AlbumsTags at on a.Id = at.AlbumsId" +
        "join Tags t on t.Id = at.TagsId" +
        "where" +
        "t.Name like 'string')");
                var tmp22 = tmp.ToList();

                //var tmp = await (from a in _context.Albums
                //          where (a.Tags.Any(at => EF.Functions.Like(at.Name, request.Tag)))
                //          select a.Id.ToString()).ToListAsync();

                //var tmp3 = _context.Albums.Where(a => a.Tags.Any(at => EF.Functions.Like(at.Name, request.Tag)));

                //photoListQuery = (from p in _context.Photos
                //                 where (p.Tags.Any(t => EF.Functions.Like(t.Name, request.Tag)))                                 
                //                 select new PhotoDto
                //                 {
                //                     Id = p.Id,
                //                     FileName = p.FileName,
                //                     AddDate = p.AddDate,
                //                     Description = p.Description,
                //                     AlbumId = p.AlbumId
                //                 });
                ////photoListQuery.Union()
                //photoListQuery =
                //_context.Photos
                //.Select(p => new PhotoDto
                //{
                //    Id = p.Id,
                //    FileName = p.FileName,
                //    AddDate = p.AddDate,
                //    Description = p.Description,
                //    AlbumId = p.AlbumId
                //})
                //.Where(p => _context.Albums.Where(a => a.Tags.Any(at => EF.Functions.Like(at.Name, request.Tag))).Select(a => a.Id.ToString()).ToList().Contains(p.AlbumId.ToString()));
                //        // _context.Albums.Where(a=>a.Tags.Any(at => EF.Functions.Like(at.Name,request.Tag)))
                //       // );
                //var tmpQ = photoListQuery.ToString();
                //var tmpQuery = photoListQuery.ToQueryString();
                //return await Task.FromResult(photoListQuery);
                return null;
            }
        }
    }
}
