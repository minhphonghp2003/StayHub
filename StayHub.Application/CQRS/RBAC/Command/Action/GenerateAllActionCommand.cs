using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;

namespace StayHub.Application.CQRS.RBAC.Command.Action
{
    // Include properties to be used as input for the command
    public record GenerateAllActionCommand() : IRequest<BaseResponse<List<AllActionDTO>>>;
    public sealed class GenerateAllActionCommandHandler(IEnumerable<EndpointDataSource> endpointSources, IActionRepository actionRepository) : BaseResponseHandler, IRequestHandler<GenerateAllActionCommand, BaseResponse<List<AllActionDTO>>>
    {
        public async Task<BaseResponse<List<AllActionDTO>>> Handle(GenerateAllActionCommand request, CancellationToken cancellationToken)
        {
            var endpoints = endpointSources.SelectMany(s => s.Endpoints).OfType<RouteEndpoint>().Where(e => e.Metadata.OfType<HttpMethodMetadata>().Any()).Select(e =>
           new AllActionDTO
           {
               Path = e.RoutePattern.RawText,
               Methods = e.Metadata
                    .OfType<HttpMethodMetadata>()
                    .FirstOrDefault()?.HttpMethods
           });
            return Success<List<AllActionDTO>>(data: endpoints.ToList());
        }
    }

}