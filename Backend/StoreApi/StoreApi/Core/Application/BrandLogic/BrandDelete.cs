using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.BrandLogic
{
    public class BrandDelete
    {
        public class BrandDeleteCommand : IRequest<bool>
        {
            public string id { get; set; }
        }

        public class BrandDeleteValidator : AbstractValidator<BrandDeleteCommand>
        {
            public BrandDeleteValidator()
            {
                RuleFor(x => x.id).NotNull().WithMessage("La marca se eliminó exitosamente"); ;
            }
        }

        public class BrandDeleteHandler : IRequestHandler<BrandDeleteCommand, bool>
        {
            public readonly IMongoRepository<Brand> dbProduct;

            public BrandDeleteHandler(IMongoRepository<Brand> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<bool> Handle(BrandDeleteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.DeleteById(request.id);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
