using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.Catalog.Command.Category;
using StayHub.Application.CQRS.Catalog.Query.Category;

namespace StayHub.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int? pageNumber, int? pageSize, string? search)
        {
            return GenerateResponse(await Mediator.Send(new GetAllCategoryQuery( pageNumber,pageSize,search )));
        }
        [HttpGet("flat")]
        public async Task<IActionResult> GetAllFlatAsync()
        {
            return GenerateResponse(await Mediator.Send(new GetAllFlatCategoryQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(AddCategoryCommand request)
        {
            return GenerateResponse(await Mediator.Send(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryCommand request)
        {
            request.Id = id;
            return GenerateResponse(await Mediator.Send(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return GenerateResponse(await Mediator.Send(new DeleteCategoryCommand(id)));
        }
    }
}

