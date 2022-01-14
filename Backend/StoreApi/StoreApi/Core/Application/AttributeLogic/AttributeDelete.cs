using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Attribute = StoreApi.Core.Domain.Attribute;

namespace StoreApi.Core.Application.AttributeLogic
{
    public class AttributeDelete
    {
        public class AttributeDeleteCommand : IRequest<bool>
        {
            public string id { get; set; }
        }

        public class AttributeDeleteValidator : AbstractValidator<AttributeDeleteCommand>
        {
            public AttributeDeleteValidator()
            {
                RuleFor(x => x.id).NotNull().WithMessage("El atributo se eliminó exitosamente"); ;
            }
        }

        public class AttributeDeleteHandler : IRequestHandler<AttributeDeleteCommand, bool>
        {
            public readonly IMongoRepository<Attribute> dbProduct;

            public AttributeDeleteHandler(IMongoRepository<Attribute> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<bool> Handle(AttributeDeleteCommand request, CancellationToken cancellationToken)
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
