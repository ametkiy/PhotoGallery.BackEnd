using LightQuery.Client;
using LightQuery.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using PhotoGallery.Features.PhotoFeatures.Commands;
using PhotoGallery.Features.PhotoFeatures.Queries;
using PhotoGallery.Features.Photos.Commands;
using PhotoGallery.Features.Photos.Queries;
using PhotoGallery.Model.DTO;
using PhotoGallery.Model.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhotoGallery.Controllers
{
    [ApiController]
    [Route("/api/photos")]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class PhotosController : Controller
    {
        private IMediator _mediator;
        private UserManager<ApplicationUser> _userManager;

        public PhotosController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            this._mediator = mediator;
            this._userManager = userManager;
        }

        [AsyncLightQuery(forcePagination: false, defaultPageSize: 10)]
        [ProducesResponseType(typeof(PaginationResult<PhotoDto>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = _userManager.GetUserId(this.User);
            var result = await _mediator.Send(new GetPhotosQuery { UserId= userId });
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
            var userId = _userManager.GetUserId(this.User);
            var result = (await _mediator.Send(new CreatePhotoCommand { FormFiles = Files, AlbumId = AlbumId, UserId = userId })) ; ;
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
            var userId = _userManager.GetUserId(this.User);
            var result = await _mediator.Send(new DeletePhotoByIdCommand { Id = id, UserId = userId });
            return Ok(result);
        }
    }
}
