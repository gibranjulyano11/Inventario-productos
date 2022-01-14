using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.ProductLogic
{
    public class ProductDelete
    {
        public class ProductDeleteCommand : IRequest<bool>
        {
            public string id { get; set; }
        }

        public class ProductDeleteValidator : AbstractValidator<ProductDeleteCommand>
        {
            public ProductDeleteValidator()
            {
                RuleFor(x => x.id).NotNull().WithMessage("El producto se eliminó exitosamente"); ;
            }
        }

        public class ProductDeleteHandler : IRequestHandler<ProductDeleteCommand, bool>
        {
            public readonly IMongoRepository<Product> dbProduct;

            public ProductDeleteHandler(IMongoRepository<Product> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<bool> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.DeleteById(request.id);

                    return true;
                }
                catch (Exception )
                {
                    return false;
                }
            }
        }
    }
}
