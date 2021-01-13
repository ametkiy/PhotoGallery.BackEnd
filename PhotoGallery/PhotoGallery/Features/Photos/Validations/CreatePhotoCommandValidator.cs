using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PhotoGallery.Features.PhotoFeatures.Commands;


namespace PhotoGallery.Features.Photos.Validations
{
    public class CreatePhotoCommandValidator : AbstractValidator<CreatePhotoCommand>
    {
        public CreatePhotoCommandValidator(IConfiguration config)
        {          
            //RuleForEach(x => x.FormFiles).SetValidator(new FileValidator(config));
        }
    }
    public class FileValidator : AbstractValidator<IFormFile>
    {
        //public FileValidator(IConfiguration config)
        //{
        //    var fileSizeLimit = config.GetValue<long>("FileSizeLimit");

        //    RuleFor(x => x.Length).LessThan(1);

        //    if (fileSizeLimit > 0)
        //        RuleFor(x => x.Length).NotNull().GreaterThan(fileSizeLimit);
        //            //.WithMessage("File size is larger than allowed");

        //    RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
        //        .WithMessage("File type is larger than allowed");


        //}
    }

}
