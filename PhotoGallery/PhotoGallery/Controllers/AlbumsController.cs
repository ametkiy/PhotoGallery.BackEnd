using LightQuery.Client;
using LightQuery.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Features.AlbumFeatures.Commands;
using PhotoGallery.Features.AlbumFeatures.Queries;
using PhotoGallery.Features.PhotoFeatures.Queries;
using PhotoGallery.Model.DTO;
using System;
using System.Threading.Tasks;

namespace PhotoGallery.Controllers
{
    [ApiController]
    [Route("/api/albums")]
    public class AlbumsController : ControllerBase
    {
        private IMediator _mediator;

        public AlbumsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllAlbumsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetAlbumByIdQuery { Id = id });
            return Ok(result);
        }

        [AsyncLightQuery(forcePagination: false, defaultPageSize: 10)]
        [ProducesResponseType(typeof(PaginationResult<PhotoDto>), 200)]
        [HttpGet("{id}/photos")]
        public async Task<IActionResult> GetByAlbumId(Guid id)
        {
            var result = await _mediator.Send(new GetPhotosInAlbumsQuery { AlbumId = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAlbumCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateAlbumCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteAlbumByIdCommand { Id = id });
            return Ok(result);
        }
    }
}
