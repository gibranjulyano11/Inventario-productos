using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Core.Application.CategoryLogic;
using StoreApi.Core.Application.ProductLogic;
using System.Threading.Tasks;

namespace StoreApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]

    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(CategoryCreate.CategoryCreateCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new CategoryGet()));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await mediator.Send(new CategoryDelete.CategoryDeleteCommand
            {
                id = id
            }));
        }

        [HttpPut]
        public async Task<ActionResult> Update(CategoryUpdate.CategoryUpdateCommand command)
        {
            return Ok(await mediator.Send(command));
        }

    }
}
