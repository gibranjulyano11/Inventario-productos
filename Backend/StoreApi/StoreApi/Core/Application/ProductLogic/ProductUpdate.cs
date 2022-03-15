using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Components;
using MongoDB.Driver;
using StoreApi.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.ProductLogic
{
  public class ProductUpdate
    {
        public class ProductUpdateCommand : IRequest<string>
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Price { get; set; }
            public string Category { get; set; }
            public string Brand { get; set; }
            public string Attribute { get; set; }

        }
        public class ProductUpdateValidator : AbstractValidator<ProductUpdateCommand>
        {
            public ProductUpdateValidator()
            {
                RuleFor(x => x.Name).NotEmpty().NotNull();
                RuleFor(x => x.Code).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
                RuleFor(x => x.Price).NotEmpty().NotNull().WithMessage("Error, el producto necesita precio");
                RuleFor(x => x.Category).NotEmpty().NotNull().WithMessage("Error, el producto necesita una categoría");
                RuleFor(x => x.Brand).NotEmpty().NotNull().WithMessage("Error, el producto necesita una marca");
                RuleFor(x => x.Attribute).NotEmpty().NotNull().WithMessage("Error, el producto necesita un atributo");
            }
        }
        public class ProductUpdateHandler : IRequestHandler<ProductUpdateCommand, string>
        {
            private readonly IMongoRepository<Product> dbProduct;

            public ProductUpdateHandler(IMongoRepository<Product> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<string> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.UpdateReplaceDocument(new Product
                    {
                        Id = request.Id,
                        Name = request.Name,
                        Price = request.Price,
                        Code = request.Code,
                        Category = request.Category,
                        Brand = request.Brand,
                        Attribute = request.Attribute,
                    });


                    return $"El producto {request.Name} fue actualizado exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
