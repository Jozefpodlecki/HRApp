using HRApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.DAL.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _dbContext;

        public PersonRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Person>> GetForPersonIdAsync(
            Guid personId,
            int top = 10,
            int offset = 0)
        {
            var person = await _dbContext.People.FindAsync(personId);

            var result = await _dbContext
                .People
                .Where(pr => pr.HierarchyId.IsDescendantOf(person.HierarchyId))
                .Take(top)
                .Skip(offset)
                .ToListAsync();

            return result;
        }

        public Task<PersonStats> GetStatsForPersonIdAsync(Guid personId, int year) => _dbContext
            .PersonStats.FirstOrDefaultAsync(pr => pr.Id == personId && pr.Year == year);
    }
}
