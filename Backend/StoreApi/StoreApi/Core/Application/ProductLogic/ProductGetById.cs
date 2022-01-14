//using Lib.Service.Mongo.Interfaces;
//using MediatR;
//using StoreApi.Core.Domain;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;


//namespace StoreApi.Core.Application.ProductLogic
//{
//    public class ProductGetById : IRequest<Product>
//    {
//        public class GetByIdProductHandler : IRequestHandler<ProductId>
//        {
//            private readonly IMongoRepository<Product> dbProduct;
//            public GetByIdProductHandler(IMongoRepository<Product> dbProduct)
//            {
//                this.dbProduct = dbProduct;
//            }

//            public async Task<Product> GetProductById(string id, CancellationToken cancellationToken)
//            {
//                var productN = await dbProduct.GetById(id);
//                return productN;
//            }
//        }
//    }
//}
