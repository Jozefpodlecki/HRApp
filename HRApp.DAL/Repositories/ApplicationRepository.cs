using HRApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.DAL.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppDbContext _dbContext;

        public ApplicationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AnnualLeaveApplication>> GetForPersonAsync(Guid userId, int top = 10, int offset = 0) =>
            await _dbContext.AnnualLeaveApplications
                .Where(pr => pr.CreatedById == userId)
                .Take(top)
                .Skip(offset)
                .ToListAsync();

        public async Task<AnnualLeaveApplication> CreateAsync(AnnualLeaveApplication entity)
        {
            _dbContext.AnnualLeaveApplications.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public ValueTask<AnnualLeaveApplication> GetByIdAsync(int id) => _dbContext.AnnualLeaveApplications.FindAsync(id);
    }
}