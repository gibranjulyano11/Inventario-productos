using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using StoreApi.Core.Application.BrandLogic;
using StoreApi.Core.Application.ProductLogic;
using StoreApi.Core.Domain;
using System.Threading.Tasks;

namespace StoreApi.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator mediator;

        public BrandsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(BrandCreate.BrandCreateCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new BrandGet()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> Get(string Id)
        {
            var filter = Builders<Brand>.Filter.Eq(doc => doc.Id, Id);
            return Ok(await mediator.Send(new BrandGet()));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await mediator.Send(new BrandDelete.BrandDeleteCommand
            {
                id = id
            }));
        }

        [HttpPut]
        public async Task<ActionResult> Update(BrandUpdate.BrandUpdateCommand command)
        {
            return Ok(await mediator.Send(command));
        }

    }
}
