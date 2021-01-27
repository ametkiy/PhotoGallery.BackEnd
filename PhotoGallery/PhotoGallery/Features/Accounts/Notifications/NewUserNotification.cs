using MediatR;
using Microsoft.AspNetCore.Identity;
using PhotoGallery.Exceptions;
using PhotoGallery.Model.Entities;
using PhotoGallery.Utils;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PhotoGallery.Features.Accounts.Notifications
{
    public class NewUserNotification : INotification
    {
        public Guid ID { get; set; }

        public class ConfirmEmailNotificationHandler : INotificationHandler<NewUserNotification>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IConfiguration _config;
            public ConfirmEmailNotificationHandler(UserManager<ApplicationUser> userManager, IConfiguration config)
            {
                _userManager = userManager;
                _config = config;
            }
            public async Task Handle(NewUserNotification notification, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(notification.ID.ToString());

                if (user == null)
                    throw new CreateNewUserException("Can't send email. User not found.");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var url = _config.GetValue<string>("ServerUrl") + $"/api/accounts/confirmationcode?id={notification.ID}&code={code}";

                EmailService emailService = new EmailService(_config);
                await emailService.SendEmailAsync(user.Email, "Confirm your account",
                    $"<a href='{url}'>Confirm registration by clicking on this link!</a>");

                Debug.WriteLine("Send email");
            }
        }
    }
}
