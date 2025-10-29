using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayHub.Application.CQRS.RBAC.Query.User;

namespace StayHub.API.Controllers.RBAC
{
    [Route(".well-known")]
    [AllowAnonymous]
    public class JWKController:BaseController
    {
        [HttpGet("jwks.json")]
        public async Task<IActionResult> GetJWKS()
        {
            return Ok(await Mediator.Send(new GetJWKQuery()));
        }
    }
}
