using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.CQRS.RBAC.Query.Menu
{
    public record GetAllMenuOfTier(int tierId) : IRequest<Response<List<int>>>;
    public sealed class GetAllMenuOfTierHandler(IMenuRepository menuRepository) : BaseResponseHandler, IRequestHandler<GetAllMenuOfTier, Response<List<int>>>
    {
        public async Task<Response<List<int>>> Handle(GetAllMenuOfTier request, CancellationToken cancellationToken)
        {
            return Success(data: await menuRepository.GetMenusOfTier(request.tierId));
       }
    }
}
