using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;
using PhotoGallery.Data;
using PhotoGallery.Features.Accounts.Commands;
using PhotoGallery.Model.DTO;
using PhotoGallery.Model.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Controllers
{
    [ApiController]
    [Route("/api/accounts")]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class AccountsController : Controller
    {
        private IMediator _mediator;
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(IMediator mediator, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this._mediator = mediator;
            this._userManager = userManager;
            this._mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Create(CreateAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("userInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var tmpUser = await _userManager.GetUserAsync(this.User);
            var userInfoDto = _mapper.Map<UserInfoDto>(tmpUser);
            return Ok(userInfoDto);
        }

    }
}
