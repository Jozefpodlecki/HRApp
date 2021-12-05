using HRApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.DAL.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);

        Task<IEnumerable<Role>> GetRolesForUser(Guid userId);
    }
}
