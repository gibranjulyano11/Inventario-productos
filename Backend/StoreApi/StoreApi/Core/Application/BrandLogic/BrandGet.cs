using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.BrandLogic
{
    public class BrandGet : IRequest<IEnumerable<Brand>>
    {
        public class GetAllBrandHandler : IRequestHandler<BrandGet, IEnumerable<Brand>>
        {
            private readonly IMongoRepository<Brand> dbProduct;
            public GetAllBrandHandler(IMongoRepository<Brand> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<IEnumerable<Brand>> Handle(BrandGet request, CancellationToken cancellationToken)
            {
                var brandList = await dbProduct.GetAll();
                return brandList;
            }
        }
    }
}
