using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Common;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.Action
{
    // Include properties to be used as input for the command
    public record GenerateAllActionCommand() : IRequest<BaseResponse<List<ActionDTO>>>;
    public sealed class GenerateAllActionCommandHandler(IEnumerable<EndpointDataSource> endpointSources, IActionRepository actionRepository) : BaseResponseHandler, IRequestHandler<GenerateAllActionCommand, BaseResponse<List<ActionDTO>>>
    {
        public async Task<BaseResponse<List<ActionDTO>>> Handle(GenerateAllActionCommand request, CancellationToken cancellationToken)
        {
            var endpoints = endpointSources.SelectMany(s => s.Endpoints).OfType<RouteEndpoint>().Where(e => e.Metadata.OfType<HttpMethodMetadata>().Any()).Select(e =>
           new StayHub.Domain.Entity.RBAC.Action
           {
               Path = e.RoutePattern.RawText,
               AllowAnonymous = e.Metadata.GetMetadata<AllowAnonymousAttribute>() != null,
               Method = (HttpVerb)Enum.Parse(typeof(HttpVerb), e.Metadata
                    .OfType<HttpMethodMetadata>()
                    .FirstOrDefault()?.HttpMethods.FirstOrDefault().ToUpper() ?? "GET"
)
           });
            var result = await actionRepository.AddRangeIfNotExitsAsync(endpoints.ToList(), (newAction, existingActions) =>
                !existingActions.Any(e => e.Path == newAction.Path && e.Method == newAction.Method)
            );
            if (result.Any())
            {
                return Success<List<ActionDTO>>(data: result.Select(e => new ActionDTO
                {
                    Path = e.Path,
                    AllowAnonymous = e.AllowAnonymous,
                    Method = e.Method.ToString()
                }).ToList());
            }
            return Success<List<ActionDTO>>(data: [], message: "No new actions were generated.");
        }
    }

}