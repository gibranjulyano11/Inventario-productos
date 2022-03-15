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
            public string Id { get; set; }
            public string Name { get; set; }

        }
        public class BrandUpdateValidator : AbstractValidator<BrandUpdateCommand>
        {
            public BrandUpdateValidator()
            {
                RuleFor(x => x.Id).NotEmpty().NotNull();
                RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
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
                        Id = request.Id,
                        Name = request.Name,

                    });

                    return $"La Marca {request.Name} fue actualizada exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
