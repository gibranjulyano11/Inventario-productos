using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using StoreApi.Core.Application.ProductLogic;
using StoreApi.Core.Domain;
using System.Threading.Tasks;

namespace StoreApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<Product>> Create(ProductCreate.ProductCreateCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new ProductGet()));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(string Id) 
        {
            var filter = Builders<Product>.Filter.Eq(doc => doc.Id, Id);
            return Ok(await mediator.Send(new ProductGet()));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string Id)
        {
            return Ok(await mediator.Send(new ProductDelete.ProductDeleteCommand
            {
              Id = Id
            }));
        }

        [HttpPut]
        public async Task<ActionResult> Update(ProductUpdate.ProductUpdateCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
