using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.BrandLogic
{
    public class BrandCreate
    {
        public class BrandCreateCommand : IRequest<string>
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class BrandCreateValidator : AbstractValidator<BrandCreateCommand>
        {
            public BrandCreateValidator()
            {
                RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Error, ingrese el nombre de una marca");
            }
        }

        public class BrandCreateHandler : IRequestHandler<BrandCreateCommand, string>
        {
            private readonly IMongoRepository<Brand> dbProduct;

            public BrandCreateHandler(IMongoRepository<Brand> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<string> Handle(BrandCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.InsertDocument(new Brand
                    {
                        Name = request.Name,
                    });


                    return $"La marca {request.Name} fue insertada exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }

    }
}
