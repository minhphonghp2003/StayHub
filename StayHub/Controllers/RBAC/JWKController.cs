using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Query.Token;

namespace StayHub.API.Controllers.RBAC
{
    [Route(".well-known")]
    public class JWKController:BaseController
    {
        [HttpGet("jwks.json")]
        public async Task<IActionResult> GetJWKS()
        {
            return Ok(await Mediator.Send(new GetJWKQuery()));
        }
    }
}
