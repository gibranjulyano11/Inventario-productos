
using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.ProductLogic
{
  public class ProductCreate
    {
        public class ProductCreateCommand : IRequest<string>
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Price { get; set; }
            public string Category { get; set; }
            public string Brand { get; set; }
            public string Attribute { get; set; }
        }

        public class ProductCreateValidator : AbstractValidator<ProductCreateCommand>
        {
            public ProductCreateValidator()
            {
                RuleFor(x => x.Name).NotEmpty().NotNull();
                RuleFor(x => x.Code).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
                RuleFor(x => x.Price).NotEmpty().NotNull().WithMessage("Error, el producto necesita precio");
                RuleFor(x => x.Category).NotEmpty().NotNull().WithMessage("Error, el producto necesita una categoría");
                RuleFor(x => x.Brand).NotEmpty().NotNull().WithMessage("Error, el producto necesita una marca");
                RuleFor(x => x.Attribute).NotEmpty().NotNull().WithMessage("Error, el producto necesita un atributo");
            }
        }

        public class ProductCreateHandler : IRequestHandler<ProductCreateCommand, string>
        {
            private readonly IMongoRepository<Product> dbProduct;

            public ProductCreateHandler(IMongoRepository<Product> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<string> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.InsertDocument(new Product
                    {
                        Name = request.Name,
                        Code = request.Code,
                        Price = request.Price,
                        Category = request.Category,
                        Brand = request.Brand,
                        Attribute = request.Attribute,
                    });


                    return $"El producto {request.Name} fue insertado exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
