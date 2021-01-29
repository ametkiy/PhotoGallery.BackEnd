using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using PhotoGallery.Features.Likes.Queries;
using PhotoGallery.Features.Photos.Commands;
using PhotoGallery.Model.Entities;
using System;
using System.Threading.Tasks;

namespace PhotoGallery.Controllers
{
    [Route("/api/likes")]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class LikesController : Controller
    {
        private IMediator _mediator;
        private UserManager<ApplicationUser> _userManager;

        public LikesController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            this._mediator = mediator;
            this._userManager = userManager;
        }

        [HttpGet("usersListLikedPhoto/{photoId}")]
        public async Task<IActionResult> GetUsersListLikedPhotoByPhotoId(Guid photoId)
        {
            var result = await _mediator.Send(new GetUsersLikedPhoto { PhotoId = photoId });
            return Ok(result);
        }

        [HttpGet("photoLikesCount/{photoId}")]
        public async Task<IActionResult> GetPhotoLikesCount(Guid photoId)
        {
            var result = await _mediator.Send(new GetPhotoLikesCount { PhotoId = photoId });
            return Ok(result);
        }

        [HttpGet("userLikedPhoto/{photoId}")]
        public async Task<IActionResult> UserLikedPhotoByPhotoId(Guid photoId)
        {
            var userId = _userManager.GetUserId(this.User);
            var result = await _mediator.Send(new UserLikedPhoto { PhotoId = photoId, UserId = userId });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SetLike([FromBody]Like like)
        {
            var userId = _userManager.GetUserId(this.User);

            var result = await _mediator.Send(new SetLike { PhotoId = Guid.Parse(like.Id), UserId = userId });
            return Ok(result);
        }

        public class Like
        {
            public string Id { get; set; }
        }

    }
}
