using Lib.Service.Mongo.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using StoreApi.Core.Application.AttributeLogic;
using StoreApi.Core.Application.ProductLogic;
using System;
using System.Threading.Tasks;


namespace StoreApi.Controllers
{
    [Route("api/attributes")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IMediator mediator;
        public readonly IMongoCollection<Pagination> collection;

        public AttributesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(AttributeCreate.AttributeCreateCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new AttributeGet()));
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Attribute>> Get(string Id)
        //{
        //    var filter = Builders<Attribute>.Filter.Eq(doc => doc.Id, Id);
        //    return Ok(await mediator.Send(new AttributeGet()));
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await mediator.Send(new AttributeDelete.AttributeDeleteCommand
            {
                id = id
            }));
        }

        [HttpPut]
        public async Task<ActionResult> Update(AttributeUpdate.AttributeUpdateCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        //[HttpGet("atributes/{id}")]
        //public async Task<PaginationEntity<Pagination>> GetByPagination(PaginationEntity<Pagination> pagination, FilterDefinition<Pagination> filter, CancellationToken cancellationToken = default)
        //{
        //    var resp = await collection
        //                .Find(filter)
        //                .Skip((pagination.page - 1) * pagination.pageSize)
        //                .Limit(pagination.pageSize)
        //                .ToListAsync(cancellationToken: cancellationToken);

        //    var totalDocuments = (await collection.CountDocumentsAsync(filter));

        //    var rounded = Math.Ceiling(totalDocuments / Convert.ToDecimal(pagination.pageSize));

        //    var totalPages = Convert.ToInt32(rounded);

        //    pagination.totalPages = totalPages;
        //    pagination.totalRows = Convert.ToInt32(totalDocuments);
        //    pagination.data = resp;

        //    return pagination;
        //}

    }
}
