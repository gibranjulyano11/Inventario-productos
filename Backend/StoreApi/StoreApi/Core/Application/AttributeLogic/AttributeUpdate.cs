using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using MongoDB.Driver;
using StoreApi.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.ProductLogic
{
    public class AttributeUpdate
    {
        public class AttributeUpdateCommand : IRequest<string>
        {
            public string Id { get; set; }
            public string Name { get; set; }

        }
        public class AttributeUpdateValidator : AbstractValidator<AttributeUpdateCommand>
        {
            public AttributeUpdateValidator()
            {
                RuleFor(x => x.Id).NotEmpty().NotNull();
                RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
            }
        }
        public class AttributeUpdateHandler : IRequestHandler<AttributeUpdateCommand, string>
        {
            private readonly IMongoRepository<Attribute> dbProduct;

            public AttributeUpdateHandler(IMongoRepository<Attribute> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<string> Handle(AttributeUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.UpdateReplaceDocument(new Attribute
                    {
                        Id = request.Id,
                        Name = request.Name,
                    });


                    return $"El Atributo {request.Name} fue actualizado exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
