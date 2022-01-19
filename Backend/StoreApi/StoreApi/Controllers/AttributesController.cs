using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Core.Application.AttributeLogic;
using StoreApi.Core.Application.ProductLogic;
using System.Threading.Tasks;

namespace StoreApi.Controllers
{
    [Route("api/attributes")]
    [ApiController]
    [Authorize]

    public class AttributesController : ControllerBase
    {
        private readonly IMediator mediator;

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

    }
}