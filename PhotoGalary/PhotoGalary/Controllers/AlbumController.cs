using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhotoGalary.Features.AlbumFeatures.Commands;
using PhotoGalary.Features.AlbumFeatures.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllAlbumsQuery()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetAlbumByIdQuery { Id = id }));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteAlbumByIdCommand { Id = id }));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAlbumCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }
    }
}
