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

        //private readonly PhotoGalleryContext _context;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private static bool _databaseChecked;
        //private readonly IConfiguration _configuration;

        //    public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, PhotoGalleryContext context, IConfiguration configuration)
        //    {
        //        _userManager = userManager;
        //        _signInManager = signInManager;
        //        _context = context;
        //        _configuration = configuration;
        //}
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

        //[HttpPost("register")]
        //public async Task<IActionResult> Create(CreateAccountCommand command)
        //{
        //    var user = new ApplicationUser { UserName = command.Email };
        //    IdentityResult result = await _userManager.CreateAsync(user, command.Password);
        //    if (result.Succeeded)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }


        //    //var result = await _mediator.Send(command);
        //    //return Ok(result);
        //}

        //[HttpPost("login")]
        //public async Task<ActionResult<UserModel>> LoginAsync(LoginQuery query)
        //{
        //    var result = await _mediator.Send(query);
        //    return result;
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login(string email, string password)
        //{
        //    var user = await _userManager.FindByNameAsync(email);
        //    if (user != null && await _userManager.CheckPasswordAsync(user, password))
        //    {
        //        //var userRoles = await _userManager.GetRolesAsync(user);

        //        var authClaims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, user.UserName),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        };

        //        //foreach (var userRole in userRoles)
        //        //{
        //        //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        //        //}

        //        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        //        var token = new JwtSecurityToken(
        //            issuer: _configuration["JWT:ValidIssuer"],
        //            audience: _configuration["JWT:ValidAudience"],
        //            expires: DateTime.Now.AddHours(3),
        //            claims: authClaims,
        //            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //            );

        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token),
        //            expiration = token.ValidTo
        //        });
        //    }
        //    return Unauthorized();
        //    //EnsureDatabaseCreated(_context);
        //    //var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
        //    //if (result.Succeeded)
        //    //{
        //    //    return Ok();
        //    //}
        //    //if (result.RequiresTwoFactor)
        //    //{
        //    //    return Ok();
        //    //}
        //    //if (result.IsLockedOut)
        //    //{
        //    //    return ValidationProblem();
        //    //}
        //    //else
        //    //{
        //    //    return ValidationProblem();
        //    //}
        //}

        //[HttpPost("logoff")]
        //public async Task<IActionResult> LogOff()
        //{
        //    await _signInManager.SignOutAsync();
        //    return Ok();
        //}


        //private static void EnsureDatabaseCreated(PhotoGalleryContext context)
        //{
        //    if (!_databaseChecked)
        //    {
        //        _databaseChecked = true;
        //        context.Database.EnsureCreated();
        //    }
        //}

    }
}
