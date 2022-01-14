using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.TagLogic
{
    public class TagGet : IRequest<IEnumerable<Tag>>
    {
        public class GetAllTagHandler : IRequestHandler<TagGet, IEnumerable<Tag>>
        {
            private readonly IMongoRepository<Tag> dbProduct;
            public GetAllTagHandler(IMongoRepository<Tag> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<IEnumerable<Tag>> Handle(TagGet request, CancellationToken cancellationToken)
            {
                var tagList = await dbProduct.GetAll();
                return tagList;
            }
        }
    }
}