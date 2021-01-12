using AutoMapper;
using MediatR;
using PhotoGallery.Data;
using PhotoGallery.Exceptions;
using PhotoGallery.Model.DTO;
using PhotoGallery.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoGallery.Features.Tags.Commands
{
    public class CreateTagCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Guid>
        {
            private readonly IPhotoGalleryContext _context;
            private readonly IMapper _mapper;
            public CreateTagCommandHandler(IPhotoGalleryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Guid> Handle(CreateTagCommand command, CancellationToken cancellationToken)
            {
                if (String.IsNullOrWhiteSpace(command.Name))
                {
                    throw new FieldIsEmptyException("Tag name must be completed");
                }

                Tag tag = _context.Tags.FirstOrDefault(t => t.Name.ToUpper() == command.Name.ToUpper());
                if (tag != null)
                {
                    return tag.Id;
                }
                else
                {
                    tag = new Tag();
                    tag.Name = command.Name;

                    _context.Tags.Add(tag);
                    await _context.SaveChangesAsync(cancellationToken);
                    return tag.Id;
                }
            }
        }
    }
}
