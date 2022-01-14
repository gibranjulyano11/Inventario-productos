using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.AttributeLogic
{
    public class AttributeCreate
    {
        public class AttributeCreateCommand : IRequest<string>
        {
            public string AttributeId { get; set; }
            public string AttributeName { get; set; }
        }

        public class AttributeCreateValidator : AbstractValidator<AttributeCreateCommand>
        {
            public AttributeCreateValidator()
            {
                RuleFor(x => x.AttributeName).NotEmpty().NotNull().WithMessage("Error, ingrese el nombre de una etiqueta");
            }
        }

        public class AttributeCreateHandler : IRequestHandler<AttributeCreateCommand, string>
        {
            private readonly IMongoRepository<Attribute> dbProduct;

            public AttributeCreateHandler(IMongoRepository<Attribute> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<string> Handle(AttributeCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.InsertDocument(new Attribute
                    {
                        Name = request.AttributeName,
                    });


                    return $"El atributo {request.AttributeName} fue insertado exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }

    }
}

