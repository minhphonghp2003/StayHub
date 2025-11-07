using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Command.CategoryItem
{
    public record DeleteCategoryItemCommand(int Id) : IRequest<BaseResponse<bool>>;

    public sealed class DeleteCategoryItemCommandHandler(ICategoryItemRepository categoryItemRepository)
        : BaseResponseHandler, IRequestHandler<DeleteCategoryItemCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(DeleteCategoryItemCommand request, CancellationToken cancellationToken)
        {
            await categoryItemRepository.Delete(new Domain.Entity.Catalog.CategoryItem { Id = request.Id });
            return Success(true, "CategoryItem deleted successfully");
        }
    }
}

