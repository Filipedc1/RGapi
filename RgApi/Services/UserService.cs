using Microsoft.EntityFrameworkCore;
using RgApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _repo;

        public UserService(AppDbContext context)
        {
            _repo = context;
        }

        public async Task<AppUser> GetByIdAsync(string id)
        {
            return await _repo.AppUsers
                              .Include(x => x.Claims)
                              .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser> GetByUsernameAsync(string username)
        {
            return await _repo.AppUsers
                              .Include(x => x.Claims)
                              .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task SetProfileImage(string id, string url)
        {
            var user = await GetByIdAsync(id);
            user.ProfileImageUrl = url;
            _repo.Update(user);
            await _repo.SaveChangesAsync();
        }
    }
}
