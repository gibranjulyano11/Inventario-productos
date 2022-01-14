using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.CategoryLogic
{
    public class CategoryGet : IRequest<IEnumerable<Category>>
    {
        public class GetAllCategoryHandler : IRequestHandler<CategoryGet, IEnumerable<Category>>
        {
            private readonly IMongoRepository<Category> dbProduct;
            public GetAllCategoryHandler(IMongoRepository<Category> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<IEnumerable<Category>> Handle(CategoryGet request, CancellationToken cancellationToken)
            {
                var categoryList = await dbProduct.GetAll();
                return categoryList;
            }
        }
    }
}