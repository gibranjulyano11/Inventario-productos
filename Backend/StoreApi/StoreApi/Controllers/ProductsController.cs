using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using StoreApi.Core.Application.ProductLogic;
using System.Threading;
using System.Threading.Tasks;
using static StoreApi.Core.Application.ProductLogic.ProductGet;

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
        public async Task<ActionResult<string>> Create(ProductCreate.ProductCreateCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new ProductGet()));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await mediator.Send(new ProductDelete.ProductDeleteCommand
            {
                id = id
            }));
        }

        [HttpPut]
        public async Task<ActionResult> Update(ProductUpdate.ProductUpdateCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
