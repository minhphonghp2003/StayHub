using Shared.Common;

namespace StayHub.Application.DTO.RBAC
{
    public class ActionDTO : BaseDTO
    {
        public string Path { get; set; }
        public HttpVerb Method { get; set; }
        public bool? AllowAnonymous { get; set; }
    }
}
