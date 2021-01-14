using LightQuery.Client;
using LightQuery.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Features.PhotoFeatures.Commands;
using PhotoGallery.Features.PhotoFeatures.Queries;
using PhotoGallery.Features.Photos.Queries;
using PhotoGallery.Model.DTO;
using System;
using System.Threading.Tasks;

namespace PhotoGallery.Controllers
{
    [ApiController]
    [Route("/api/photos")]
    public class PhotosController : Controller
    {
        private IMediator _mediator;

        public PhotosController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [AsyncLightQuery(forcePagination: false, defaultPageSize: 10)]
        [ProducesResponseType(typeof(PaginationResult<PhotoDto>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetPhotosQuery { });
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPhotoByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpGet("{id}/file")]
        public async Task<IActionResult> GetFileById(Guid id)
        {
            var result = await _mediator.Send(new GetPhotoFileByIdQuery { Id = id });
            FileContentResult fileResult = new FileContentResult(result.PhotoData, result.FileMimeType);
            return fileResult;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Create([FromForm] IFormFileCollection Files, [FromForm] Guid? AlbumId)
        {
            var result = (await _mediator.Send(new CreatePhotoCommand { FormFiles = Files, AlbumId = AlbumId })); ;
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePhotoCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeletePhotoByIdCommand { Id = id });
            return Ok(result);
        }
    }
}
