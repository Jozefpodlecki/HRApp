using HRApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<User?> GetByEmailAsync(string email) => _dbContext
            .Users.FirstOrDefaultAsync(pr => pr.Email == email);

        public async Task<IEnumerable<Role>> GetRolesForUserAsync(Guid userId) => await _dbContext
            .Roles.Where(pr => pr.UserRoles.Any(npr => npr.UserId == userId)).ToListAsync();
    }
}
