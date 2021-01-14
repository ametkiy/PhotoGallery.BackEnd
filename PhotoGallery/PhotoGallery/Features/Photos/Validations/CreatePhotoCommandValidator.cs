using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PhotoGallery.Features.PhotoFeatures.Commands;
using System.IO;

namespace PhotoGallery.Features.Photos.Validations
{
    public class CreatePhotoCommandValidator : AbstractValidator<CreatePhotoCommand>
    {
        public CreatePhotoCommandValidator(IConfiguration config)
        {
            RuleFor(x => x.FormFiles).Custom((list, context) =>
            {
                if (list.Count == 0)
                    context.AddFailure("FieldIsEmptyException", "No file data");
            });
            RuleForEach(x => x.FormFiles).Custom((file, context) =>
            {
                if (file.Length == 0)
                    context.AddFailure("FileSizeException", $"File {file.FileName} has no content.");

                var fileSizeLimit = config.GetValue<long>("FileSizeLimit");
                if (fileSizeLimit > 0 && file.Length > fileSizeLimit)
                    context.AddFailure("FileSizeException", $"The File '{file.FileName}' has size {file.Length} exceeds the maximum file size {fileSizeLimit}.");

                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    var fileMimeType = CheckMimeType.GetMimeType(fileBytes, file.FileName);

                    if (fileMimeType == CheckMimeType.DefaultMimeTipe)
                    {
                        context.AddFailure("UnsupportedFileFormatException", $"File '{file.FileName}' contains an unsupported data format.");
                    }
                }
            });
        }
    }
}
