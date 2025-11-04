using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;
using System.Net;

namespace StayHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        private IMediator _mediatorInstance;
        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        public IActionResult GenerateResponse<T>(BaseResponse<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response);
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.Created:
                    return CreatedAtAction(null, response);
                default:
                    return Ok(response);
            }
        }

    }
}
