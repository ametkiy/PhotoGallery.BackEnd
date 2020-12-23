using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhotoGalary.Features.AlbumFeatures.Commands;
using PhotoGalary.Features.AlbumFeatures.Queries;
using System;
using System.Threading.Tasks;

namespace PhotoGalary.Controllers
{
    [ApiController]
    [Route("/api/album")]
    public class AlbumController : ControllerBase
    {
        private IMediator _mediator;

        public AlbumController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAlbumCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteAlbumByIdCommand { Id = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateAlbumCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
