using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.Catalog.Command.CategoryItem;
using StayHub.Application.CQRS.Catalog.Query.CategoryItem;

namespace StayHub.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryItemController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetCategoryItemByIdQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await Mediator.Send(new GetAllCategoryItemQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryItem(AddCategoryItemCommand request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryItem(int id, UpdateCategoryItemCommand request)
        {
            request.Id = id;
            return GenerateResponse(await Mediator.Send(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryItem(int id)
        {
            return GenerateResponse(await Mediator.Send(new DeleteCategoryItemCommand(id)));
        }
    }
}

