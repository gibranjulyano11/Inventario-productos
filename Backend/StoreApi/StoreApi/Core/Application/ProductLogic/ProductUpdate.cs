using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
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
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public decimal ProductPrice { get; set; }
            public string ProductCategory { get; set; }
            public string ProductBrand { get; set; }

        }
        public class ProductUpdateValidator : AbstractValidator<ProductUpdateCommand>
        {
            public ProductUpdateValidator()
            {
                RuleFor(x => x.ProductName).NotEmpty().NotNull();
                RuleFor(x => x.ProductCode).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
                RuleFor(x => x.ProductPrice).NotEmpty().NotNull().WithMessage("Error, el producto necesita precio");
                RuleFor(x => x.ProductCategory).NotEmpty().NotNull().WithMessage("Error, el producto necesita una categoría");
                RuleFor(x => x.ProductBrand).NotEmpty().NotNull().WithMessage("Error, el producto necesita una marca");
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
                        Id = request.ProductId,
                        Name = request.ProductName,
                        Price = request.ProductPrice,
                        Code = request.ProductCode,
                        Category = request.ProductCategory,
                        Brand = request.ProductBrand,
                    });


                    return $"El producto {request.ProductName} fue actualizado exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
