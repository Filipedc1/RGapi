using Microsoft.EntityFrameworkCore;
using RgApi.Interfaces;
using RgApi.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _database;

        public UserService(AppDbContext context)
        {
            _database = context;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _database.AppUsers
                              .Include(x => x.Claims)
                              .Include(x => x.Address)
                              .ToListAsync();
        }

        public async Task<AppUser> GetByIdAsync(string id)
        {
            return await _database.AppUsers
                              .Include(x => x.Claims)
                              .Include(x => x.Address)
                              .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<string> GetIdAsync(string username)
        {
            var user = await _database.AppUsers
                                      .FirstOrDefaultAsync(u => u.UserName == username);

            return user.Id;
        }

        public async Task<AppUser> GetByUsernameAsync(string username)
        {
            return await _database.AppUsers
                              .Include(x => x.Claims)
                              .Include(x => x.Address)
                              .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task SetProfileImageAsync(string id, string url)
        {
            var user = await GetByIdAsync(id);
            user.ProfileImageUrl = url;
            _database.Update(user);
            await _database.SaveChangesAsync();
        }
    }
}
