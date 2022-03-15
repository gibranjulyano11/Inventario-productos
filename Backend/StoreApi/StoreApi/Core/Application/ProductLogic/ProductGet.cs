using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace StoreApi.Core.Application.ProductLogic
{
    public class ProductGet : IRequest<IEnumerable<Product>>
    {
        public class GetAllProductHandler : IRequestHandler<ProductGet, IEnumerable<Product>>
        {
            private readonly IMongoRepository<Product> dbProduct;
            public GetAllProductHandler(IMongoRepository<Product> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<IEnumerable<Product>> Handle(ProductGet request, CancellationToken cancellationToken)
            {
                var productList = await dbProduct.GetAll();
                return productList;
            }
        }
    }
}
