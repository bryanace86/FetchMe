using FetchMe.Models.PhotographerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FetchMe.Services.PhotographerServices
{
    public interface IPhotographerService
    {
        Task<List<PhotographerIndexViewModel>> GetAllAsync(PhotographerSearch search);
        Task<PhotographerDto> CreateAsync(PhotographerCreateDto input);
        Task<PhotographerDto> UpdateAsync(PhotographerUpdateDto input);
        Task<PhotographerDto> GetByUser();
        PhotographerDto GetBySlug(string slug);
        bool CheckSlugExist(string slug);
        bool UserHasPhotographer();
        Task<PhotographerUpdateDto> MapToUpdateDtoAsync(PhotographerDto photographerDto);
        PhotographerDetailsDto GetPhotographerDetails(string slug);
    }
}
