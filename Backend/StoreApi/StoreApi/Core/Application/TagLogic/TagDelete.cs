using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.TagLogic
{
    public class TagDelete
    {
        public class TagDeleteCommand : IRequest<bool>
        {
            public string id { get; set; }
        }

        public class TagDeleteValidator : AbstractValidator<TagDeleteCommand>
        {
            public TagDeleteValidator()
            {
                RuleFor(x => x.id).NotNull().WithMessage("La etiqueta se eliminó exitosamente"); ;
            }
        }

        public class TagDeleteHandler : IRequestHandler<TagDeleteCommand, bool>
        {
            public readonly IMongoRepository<Tag> dbProduct;

            public TagDeleteHandler(IMongoRepository<Tag> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<bool> Handle(TagDeleteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.DeleteById(request.id);

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}