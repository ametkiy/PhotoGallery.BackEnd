using MediatR;
using Microsoft.AspNetCore.Identity;
using PhotoGallery.Exceptions;
using PhotoGallery.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.Accounts.Commands
{
    public class CreateAccountCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, string>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            public CreateAccountCommandHandler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }
            public async Task<string> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.Email);
                if (user != null)
                {
                    throw new ConflictNewUserException(request.Email);
                }

                var newUser = new ApplicationUser { UserName = request.UserName, Email = request.Email, FirsName = request.FirstName, LastName = request.LastName };
                var result = await _userManager.CreateAsync(newUser, request.Password);
                if (result.Succeeded)
                    return newUser.Id;
                else
                    throw new CreateNewUserException(result.Errors);

            }
        }
    }
}
