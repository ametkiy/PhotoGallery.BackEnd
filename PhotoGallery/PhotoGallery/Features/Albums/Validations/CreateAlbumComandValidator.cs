using FluentValidation;
using PhotoGallery.Features.AlbumFeatures.Commands;

namespace PhotoGallery.Features.Albums.Validations
{
    public class CreateAlbumComandValidator : AbstractValidator<CreateAlbumCommand>
    {
        public CreateAlbumComandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(80);
        }
    }
}