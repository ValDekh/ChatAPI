using Chat.Application.DTOs.Contributer;
using Chat.Domain.Entities;

namespace Chat.Application.Services.Interfaces
{
    public interface IContributorService
    {
        ContributorDTOResponse ContributorDTOResponse { get; set; }

        Task<Contributor> AddContributorAsync(Guid chatId, Guid ownerId, ContributorDTORequest contributorDTORequest);
        Task DeleteAsync(Guid chatId, Guid ownerId, Guid userId);
        Task<IEnumerable<ContributorDTOResponse>> GetAllAsync(Guid chatId, Guid ownerId);
        Task<ContributorDTOResponse> GetByUserIdAsync(Guid chatId, Guid ownerId, Guid userId);
        Task UpdateAsync(Guid chatId, Guid ownerId, ContributorDTORequest newPermissionForExistContrib);
    }
}