
using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.ProductLogic
{
    public class ProductCreate
    {
        public class ProductCreateCommand : IRequest<string>
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public decimal ProductPrice { get; set; }
            public string ProductCategory { get; set; }
            public string ProductBrand { get; set; }

        }

        public class ProductCreateValidator : AbstractValidator<ProductCreateCommand>
        {
            public ProductCreateValidator()
            {
                RuleFor(x => x.ProductName).NotEmpty().NotNull();
                RuleFor(x => x.ProductCode).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
                RuleFor(x => x.ProductPrice).NotEmpty().NotNull().WithMessage("Error, el producto necesita precio");
                RuleFor(x => x.ProductCategory).NotEmpty().NotNull().WithMessage("Error, el producto necesita una categoría");
                RuleFor(x => x.ProductBrand).NotEmpty().NotNull().WithMessage("Error, el producto necesita una marca");
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
                        Name = request.ProductName,
                        Code = request.ProductCode,
                        Price = request.ProductPrice,
                        Category = request.ProductCategory,
                        Brand = request.ProductBrand,
                    });


                    return $"El producto {request.ProductName} fue insertado exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
