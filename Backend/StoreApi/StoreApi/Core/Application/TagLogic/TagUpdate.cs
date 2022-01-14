using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.ProductLogic
{
    public class TagUpdate
    {
        public class TagUpdateCommand : IRequest<string>
        {
            public string TagId { get; set; }
            public string TagName { get; set; }

        }
        public class TagUpdateValidator : AbstractValidator<TagUpdateCommand>
        {
            public TagUpdateValidator()
            {
                RuleFor(x => x.TagId).NotEmpty().NotNull();
                RuleFor(x => x.TagName).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
    
            }
        }
        public class TagUpdateHandler : IRequestHandler<TagUpdateCommand, string>
        {
            private readonly IMongoRepository<Tag> dbProduct;

            public TagUpdateHandler(IMongoRepository<Tag> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<string> Handle(TagUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.UpdateReplaceDocument(new Tag
                    {
                        Id = request.TagId,
                        Name = request.TagName,

                    });


                    return $"La Etiqueta {request.TagName} fue actualizada exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
