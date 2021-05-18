using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FetchMe.Services.PhotographServices
{
    public interface IPhotographService
    {
        Task<List<PhotographIndexViewModel>> GetAllAsync(PhotographSearch search);
        Task<PhotographDto> CreateAsync(PhotographCreateDto input);
        Task<PhotographDto> UpdateAsync(PhotographUpdateDto input);
        Task<PhotographDto> GetByIdAsync(Guid id);
        Task<bool> DeleteById(Guid id);
        string CreateUniqueFileName(string fileName);
        string CreateImageUrl(string imageTitle);
        Task<int> GetPhotographCount();
        Task<PhotographDto> GetById(Guid id);
        Task<PhotographUpdateDto> MapToUpdateDto(PhotographDto photograph);
        Task<bool> IsOwnedByPhotographer(PhotographDto photographDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
