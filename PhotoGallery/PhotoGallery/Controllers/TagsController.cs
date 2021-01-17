using LightQuery.Client;
using LightQuery.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using PhotoGallery.Features.PhotoFeatures.Queries;
using PhotoGallery.Features.Tags.Commands;
using PhotoGallery.Features.Tags.Query;
using PhotoGallery.Model.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Controllers
{
    [ApiController]
    [Route("/api/tags")]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class TagsController : Controller
    {
        private IMediator _mediator;

        public TagsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [AsyncLightQuery(forcePagination: false, defaultPageSize: 10)]
        [ProducesResponseType(typeof(PaginationResult<PhotoDto>), 200)]
        [HttpGet("{tag}/photos")]
        public async Task<IActionResult> GetPhotosByTag(string tag = "")
        {
            IQueryable<PhotoDto> result;
            if (String.IsNullOrWhiteSpace(tag))
                result = await _mediator.Send(new GetPhotosQuery { });
            else
                result = await _mediator.Send(new GetPhotosByTagQuery { Tag = tag });
            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetTagByName(string name)
        {
            var result = await _mediator.Send(new GetTagByNameQuery { Name = name });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTagCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
