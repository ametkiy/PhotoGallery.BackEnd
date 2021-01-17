using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhotoGallery.Data;
using PhotoGallery.Features.Accounts.Commands;
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
    public class AccountsController : Controller
    {
        private IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create(CreateAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
