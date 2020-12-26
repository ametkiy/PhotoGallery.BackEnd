using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGalary.Features.PhotoFeatures.Commands;
using PhotoGalary.Features.PhotoFeatures.Queries;
using PhotoGallery;
using PhotoGallery.Features.PhotoFeatures.Queries;
using System;
using System.Collections.Generic;
using System.IO;
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
            var result = await _mediator.Send(new GetAllPhotosQuery ());
            return Ok(result);
        }

        [HttpGet("{pageSize}/{page}")]
        public async Task<IActionResult> GetPaginationPhotos(int pageSize=5,  int page=1 )
        {
            var result = await _mediator.Send(new GetPaginationPhotosQuery { Page = page, PageSize = pageSize });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPhotoByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Create([FromForm] IFormFileCollection Files)
        {
            if (Files.Count > 0)
            {
                var result = (await _mediator.Send(new CreatePhotoCommand { FormFiles = Files }));

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
