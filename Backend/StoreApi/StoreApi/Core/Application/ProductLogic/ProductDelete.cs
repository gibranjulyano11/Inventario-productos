using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Components;
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
            public string Id { get; set; }
        }

        public class ProductDeleteValidator : AbstractValidator<ProductDeleteCommand>
        {
            public ProductDeleteValidator()
            {
                RuleFor(x => x.Id).NotNull().WithMessage("El producto se eliminó exitosamente"); ;
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
                    await dbProduct.DeleteById(request.Id);

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
