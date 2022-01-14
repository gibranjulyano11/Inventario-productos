using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.TagLogic
{
    public class TagCreate
    {
        public class TagCreateCommand : IRequest<string>
        {
            public string TagId { get; set; }
            public string TagName { get; set; }
        }

        public class TagCreateValidator : AbstractValidator<TagCreateCommand>
        {
            public TagCreateValidator()
            {
                RuleFor(x => x.TagName).NotEmpty().NotNull().WithMessage("Error, ingrese el nombre de una etiqueta");
            }
        }

        public class TagCreateHandler : IRequestHandler<TagCreateCommand, string>
        {
            private readonly IMongoRepository<Tag> dbProduct;

            public TagCreateHandler(IMongoRepository<Tag> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<string> Handle(TagCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.InsertDocument(new Tag
                    {
                        Name = request.TagName,
                    });


                    return $"La etiqueta {request.TagName} fue insertada exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }

    }
}