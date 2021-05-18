using FetchMe.Models.PhotographModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.UserProfileImageService
{
    public interface IUserProfileImageService
    {
        Task<UserProfileImageDto> CreateAsync(UserProfileImageUpsert input);
    }
}
