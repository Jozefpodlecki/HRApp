using HRApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.DAL.Repositories
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<AnnualLeaveApplication>> GetForPersonAsync(Guid userId, int top = 10, int offset = 0);

        Task<AnnualLeaveApplication> CreateAsync(AnnualLeaveApplication entity);

        ValueTask<AnnualLeaveApplication> GetByIdAsync(int id);
    }
}
