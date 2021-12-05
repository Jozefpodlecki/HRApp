using HRApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.DAL.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetForPersonIdAsync(Guid personId, int top = 10, int offset = 0);

        Task<PersonStats> GetStatsForPersonIdAsync(Guid personId, int year);
    }
}
