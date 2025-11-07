using MediatR;
using Shared.Response;
using StayHub.Application.Interfaces.Repository.Catalog;

namespace StayHub.Application.CQRS.Catalog.Command.Category
{
    public record DeleteCategoryCommand(int Id) : IRequest<BaseResponse<bool>>;

    public sealed class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        : BaseResponseHandler, IRequestHandler<DeleteCategoryCommand, BaseResponse<bool>>
    {
        public async Task<BaseResponse<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoryRepository.Delete(new Domain.Entity.Catalog.Category { Id = request.Id });
            return Success(true, "Category deleted successfully");
        }
    }
}

