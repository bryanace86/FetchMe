using FetchMe.Models.LikeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchMe.Services.LikeServices
{
    public interface ILikeService
    {
        Task<bool> CreateLike(AlterLikeDto like);
        Task<bool> DeleteLike(AlterLikeDto like);
    }
}
