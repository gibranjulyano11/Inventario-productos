using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.AttributeLogic
{
    public class AttributeGet : IRequest<IEnumerable<Attribute>>
    {
        public class GetAllAttributeHandler : IRequestHandler<AttributeGet, IEnumerable<Attribute>>
        {
            private readonly IMongoRepository<Attribute> dbProduct;
            public GetAllAttributeHandler(IMongoRepository<Attribute> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<IEnumerable<Attribute>> Handle(AttributeGet request, CancellationToken cancellationToken)
            {
                var attributeList = await dbProduct.GetAll();
                return attributeList;
            }
        }
    }
}
