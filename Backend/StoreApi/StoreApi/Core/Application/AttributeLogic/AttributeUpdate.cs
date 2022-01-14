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
            public string AttributeId { get; set; }
            public string AttributeName { get; set; }

        }
        public class AttributeUpdateValidator : AbstractValidator<AttributeUpdateCommand>
        {
            public AttributeUpdateValidator()
            {
                RuleFor(x => x.AttributeId).NotEmpty().NotNull();
                RuleFor(x => x.AttributeName).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
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
                        Id = request.AttributeId,
                        Name = request.AttributeName,
                    });


                    return $"El Atributo {request.AttributeName} fue actualizado exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
