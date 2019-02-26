using RgApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Services
{
    public interface IUser
    {
        Task<IEnumerable<AppUser>> GetAllAsync();
        Task<AppUser> GetByIdAsync(string id);
        Task<AppUser> GetByUsernameAsync(string username);
        Task SetProfileImage(string id, string url);
    }
}
