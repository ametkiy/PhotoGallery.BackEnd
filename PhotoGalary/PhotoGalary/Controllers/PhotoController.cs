using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhotoGalary.Features.PhotoFeatures.Commands;
using PhotoGalary.Features.PhotoFeatures.Queries;
using PhotoGalary.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<IActionResult> GetAll(int pageSize=5,  int page=1 )
        {
            return Ok(await _mediator.Send(new GetAllPhotosQuery { Page= page, PageSize=pageSize}));;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetPhotoByIdQuery { Id = id }));
        }

        public class Temp
        {
            [JsonProperty(PropertyName = "FileName")]
            string FileName { get; set; }

            [JsonProperty(PropertyName = "Description")] string Description { get; set; }
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Create([FromForm] IFormFile File, [FromForm] string tmp)
        {

            CreatePhotoCommand command = new CreatePhotoCommand();
            if (File!=null && File.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    File.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    command.PhotoData = fileBytes; 
                   
                }
                command.FileName = File.FileName;
                command.AddDate = DateTime.Now;
                if (command.PhotoData != null && command.FileName != "")
                    return Ok(await _mediator.Send(command));
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeletePhotoByIdCommand { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePhotoCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

    }
}
