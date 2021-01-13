using FluentValidation;
using PhotoGallery.Features.AlbumFeatures.Commands;

namespace PhotoGallery.Features.Albums.Validations
{
    public class UpdateAlbumCommandValidator : AbstractValidator<CreateAlbumCommand>
    {
        public UpdateAlbumCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(80);
            RuleForEach(x => x.Tags).ChildRules(tags => { tags.RuleFor(t => t.Id).NotEmpty(); });
        }
    }
}
