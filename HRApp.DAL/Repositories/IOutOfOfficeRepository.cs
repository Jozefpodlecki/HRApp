using HRApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.DAL.Repositories
{
    public interface IOutOfOfficeRepository
    {
        Task<OutOfOffice> CreateAsync(OutOfOffice entity);
    }
}
