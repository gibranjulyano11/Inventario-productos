using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using MongoDB.Driver;
using StoreApi.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.ProductLogic
{
    public class BrandUpdate
    {
        public class BrandUpdateCommand : IRequest<string>
        {
            public string BrandId { get; set; }
            public string BrandName { get; set; }

        }
        public class BrandUpdateValidator : AbstractValidator<BrandUpdateCommand>
        {
            public BrandUpdateValidator()
            {
                RuleFor(x => x.BrandId).NotEmpty().NotNull();
                RuleFor(x => x.BrandName).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
            }
        }
        public class BrandUpdateHandler : IRequestHandler<BrandUpdateCommand, string>
        {
            private readonly IMongoRepository<Brand> dbProduct;

            public BrandUpdateHandler(IMongoRepository<Brand> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<string> Handle(BrandUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.UpdateReplaceDocument(new Brand
                    {
                        Id = request.BrandId,
                        Name = request.BrandName,

                    });

                    return $"La Marca {request.BrandName} fue actualizada exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
