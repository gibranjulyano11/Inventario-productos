using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Core.Application.ProductLogic;
using StoreApi.Core.Application.TagLogic;
using System.Threading.Tasks;

namespace StoreApi.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IMediator mediator;

        public TagsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(TagCreate.TagCreateCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new TagGet()));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await mediator.Send(new TagDelete.TagDeleteCommand
            {
                id = id
            }));
        }

        [HttpPut]
        public async Task<ActionResult> Update(TagUpdate.TagUpdateCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
