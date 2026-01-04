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
    public record GenerateAllActionCommand() : IRequest<BaseResponse<bool>>;
    public sealed class GenerateAllActionCommandHandler(IEnumerable<EndpointDataSource> endpointSources, IActionRepository actionRepository) : BaseResponseHandler, IRequestHandler<GenerateAllActionCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(GenerateAllActionCommand request, CancellationToken cancellationToken)
        {
            var endpoints = endpointSources.SelectMany(s => s.Endpoints).OfType<RouteEndpoint>().Where(e => e.Metadata.OfType<HttpMethodMetadata>().Any()).Select(e =>
           new StayHub.Domain.Entity.RBAC.Action
           {
               Path = e.RoutePattern.RawText,
               AllowAnonymous = e.Metadata.GetMetadata<AllowAnonymousAttribute>() != null,
               Method = e.Metadata
                    .OfType<HttpMethodMetadata>()
                    .FirstOrDefault()?.HttpMethods.FirstOrDefault().ToUpper() ?? "GET"
           }).ToList();
            var paths = endpoints.Select(e => e.Path);
            var methods = endpoints.Select(e => e.Method);

            var existEndpoints =( await actionRepository.GetManyAsync(filter: e => paths.Contains(e.Path) && methods.Contains(e.Method), selector: (e, i) => new StayHub.Domain.Entity.RBAC.Action
            {
                Path = e.Path,
                Method = e.Method
            })).ToList();
            var newEndpoints = endpoints.Where(ep => !existEndpoints.Any(e => e.Path == ep.Path && e.Method == ep.Method)).ToList();
            var result = await actionRepository.AddRangeAsync(newEndpoints);
            if (result.Any())
            {
                return Success<bool>(data: true);
            }
            return Success<bool>(data: false, message: "No new actions were generated.");
        }
    }

}