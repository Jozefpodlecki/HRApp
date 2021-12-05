using HRApp.DAL.Models;
using System.Threading.Tasks;

namespace HRApp.DAL.Repositories
{
    public class OutOfOfficeRepository : IOutOfOfficeRepository
    {
        private readonly AppDbContext _dbContext;

        public OutOfOfficeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OutOfOffice> CreateAsync(OutOfOffice entity)
        {
            _dbContext.OutOfOffice.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}