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
    public record GenerateAllActionCommand() : IRequest<BaseResponse<bool>>;

    public sealed class GenerateAllActionCommandHandler(
        IEnumerable<EndpointDataSource> endpointSources,
        IActionRepository actionRepository)
        : BaseResponseHandler, IRequestHandler<GenerateAllActionCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(GenerateAllActionCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Quét toàn bộ endpoints hiện tại từ hệ thống code (App Endpoints)
            var appEndpoints = endpointSources
                .SelectMany(s => s.Endpoints)
                .OfType<RouteEndpoint>()
                .Where(e => e.Metadata.OfType<HttpMethodMetadata>().Any())
                .Select(e => new
                {
                    Path = e.RoutePattern.RawText,
                    AllowAnonymous = e.Metadata.GetMetadata<AllowAnonymousAttribute>() != null,
                    Method = e.Metadata
                        .OfType<HttpMethodMetadata>()
                        .FirstOrDefault()?.HttpMethods.FirstOrDefault()?.ToUpper() ?? "GET"
                }).ToList();

            if (!appEndpoints.Any())
            {
                return Success<bool>(data: false, message: "Không tìm thấy endpoint nào trong hệ thống.");
            }

            var dbActions = await actionRepository.GetManyAsync(
                filter: e => true, 
                selector: (e, i) => e,
                tracking: true
            );

            var dbActionsList = dbActions.ToList();

            var newActions = appEndpoints
                .Where(app => !dbActionsList.Any(db => db.Path == app.Path && db.Method == app.Method))
                .Select(app => new StayHub.Domain.Entity.RBAC.Action
                {
                    Path = app.Path,
                    Method = app.Method,
                    AllowAnonymous = app.AllowAnonymous
                }).ToList();

            var obsoleteActions = dbActionsList
                .Where(db => !appEndpoints.Any(app => app.Path == db.Path && app.Method == db.Method))
                .ToList();

            var existingActionsToUpdate = dbActionsList
                .Where(db => appEndpoints.Any(app =>
                    app.Path == db.Path &&
                    app.Method == db.Method &&
                    app.AllowAnonymous != db.AllowAnonymous))
                .ToList();

            bool hasChanges = false;

            if (newActions.Any())
            {
                await actionRepository.AddRangeAsync(newActions);
                hasChanges = true;
            }

            if (obsoleteActions.Any())
            {
                await actionRepository.DeleteWhere(e => obsoleteActions.Select(j=>j.Id).Contains(e.Id), false);
                hasChanges = true;
            }

            if (existingActionsToUpdate.Any())
            {
                foreach (var action in existingActionsToUpdate)
                {
                    var appEq = appEndpoints.First(app => app.Path == action.Path && app.Method == action.Method);
                    action.AllowAnonymous = appEq.AllowAnonymous;
                }

                hasChanges = true;
            }

            if (hasChanges)
            {
                await actionRepository.SaveAsync();

                var message =
                    $"Đồng bộ thành công: Thêm {newActions.Count}, Xóa {obsoleteActions.Count}, Cập nhật {existingActionsToUpdate.Count}.";
                return Success<bool>(data: true, message: message);
            }

            return Success<bool>(data: false, message: "Mọi thứ đã được đồng bộ, không có API nào bị thay đổi.");
        }
    }
}