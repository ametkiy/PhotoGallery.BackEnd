using LightQuery;
using LightQuery.Client;
using LightQuery.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGalary.Features.PhotoFeatures.Commands;
using PhotoGalary.Features.PhotoFeatures.Queries;
using PhotoGallery;
using PhotoGallery.Features.PhotoFeatures.Queries;
using PhotoGallery.Model.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGalary.Controllers
{
    [ApiController]
    [Route("/api/photo")]
    public class PhotoController : Controller
    {
        private IMediator _mediator;

        public PhotoController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPhotosQuery());
            return Ok(result);
        }

        [AsyncLightQuery(forcePagination: true, defaultPageSize: 10)]
        [ProducesResponseType(typeof(PaginationResult<PhotoDto>), 200)]
        [HttpGet("GetPaginationPhotos")]
        public async Task<IActionResult> GetPaginationPhotos()
        {
            var result = await _mediator.Send(new GetPaginationPhotosQuery { });
            return Ok(result);
        }

        [AsyncLightQuery(forcePagination: true, defaultPageSize: 10)]
        [ProducesResponseType(typeof(PaginationResult<PhotoDto>), 200)]
        [HttpGet("GetByAlbumId/{albumId}")]
        public async Task<IActionResult> GetByAlbumId(string albumId = "00000000-0000-0000-0000-000000000000")
        {
            var result = await _mediator.Send(new GetPaginationPhotosInAlbumsQuery { AlbumId = Guid.Parse(albumId) });
            return Ok(result);
        }

        [AsyncLightQuery(forcePagination: true, defaultPageSize: 10)]
        [ProducesResponseType(typeof(PaginationResult<PhotoDto>), 200)]
        [HttpGet("GetPhotosByTag/{tag}")]
        public async Task<IActionResult> GetPhotosByTag(string tag = "")
        {
            IQueryable<PhotoDto> result;
            if (String.IsNullOrWhiteSpace(tag))
                result = await _mediator.Send(new GetPaginationPhotosQuery { });
            else
                result = await _mediator.Send(new GetPaginationPhotosByTagQuery { Tag = tag });
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPhotoByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Create([FromForm] IFormFileCollection Files, [FromForm] Guid? AlbumId)
        {
            if (Files.Count > 0)
            {
                var result = (await _mediator.Send(new CreatePhotoCommand { FormFiles = Files, AlbumId = AlbumId })); ;

                return Ok(result);
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeletePhotoByIdCommand { Id = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePhotoCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
