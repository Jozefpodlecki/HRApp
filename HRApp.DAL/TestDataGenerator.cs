using HRApp.Common;
using HRApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HRApp.DAL
{
    public class TestDataGenerator
    {
        private readonly ISystemClock _systemClock;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AppDbContext _appDbContext;

        public TestDataGenerator(
            ISystemClock systemClock,
            IPasswordHasher passwordHasher,
            AppDbContext appDbContext)
        {
            _systemClock = systemClock;
            _passwordHasher = passwordHasher;
            _appDbContext = appDbContext;
        }

        public async Task RunAsync()
        {
            var utcNow = _systemClock.UtcNow;
            var salt = _passwordHasher.ComputeSalt();
            var passwordHash = _passwordHasher.ComputeHash("123", salt);

            var adminUser = new User("admin@test.com")
            {
                CreatedOn = utcNow,
                PasswordHash = passwordHash,
                PasswordHashSalt = salt,
            };

            salt = _passwordHasher.ComputeSalt();
            passwordHash = _passwordHasher.ComputeHash("123", salt);

            var managerUser = new User("manager@test.com")
            {
                CreatedOn = utcNow,
                PasswordHash = passwordHash,
                PasswordHashSalt = salt,
            };

            salt = _passwordHasher.ComputeSalt();
            passwordHash = _passwordHasher.ComputeHash("123", salt);

            var employeeUser1 = new User("employee1@test.com")
            {
                CreatedOn = utcNow,
                PasswordHash = passwordHash,
                PasswordHashSalt = salt,
            };

            salt = _passwordHasher.ComputeSalt();
            passwordHash = _passwordHasher.ComputeHash("123", salt);

            var employeeUser2 = new User("employee2@test.com")
            {
                CreatedOn = utcNow,
                PasswordHash = passwordHash,
                PasswordHashSalt = salt,
            };

            _appDbContext.Users.AddRange(new[] { adminUser, managerUser, employeeUser1, employeeUser2 });
            await _appDbContext.SaveChangesAsync();

            var adminRole = new Role("Admin");
            var employeeRole = new Role("Employee");
            var managerRole = new Role("Manager");

            _appDbContext.Roles.AddRange(new[] { adminRole, employeeRole, managerRole });
            await _appDbContext.SaveChangesAsync();

            var userRoles = new[]
            {
                new UserRole
                {
                    RoleId = employeeRole.Id,
                    UserId = employeeUser1.Id,
                },
                new UserRole
                {
                    RoleId = employeeRole.Id,
                    UserId = employeeUser2.Id,
                },
                new UserRole
                {
                    RoleId = managerRole.Id,
                    UserId = managerUser.Id,
                },
            };

            _appDbContext.UserRoles.AddRange(userRoles);
            await _appDbContext.SaveChangesAsync();

            var admin = new Person()
            {
                Id = adminUser.Id,
                HierarchyId = HierarchyId.Parse("/"),
            };

            var manager = new Person()
            {
                Id = adminUser.Id,
                HierarchyId = HierarchyId.Parse("/1/"),
            };

            var employee1 = new Person()
            {
                Id = adminUser.Id,
                HierarchyId = HierarchyId.Parse("/1/1"),
            };

            var employee2 = new Person()
            {
                Id = adminUser.Id,
                HierarchyId = HierarchyId.Parse("/1/2"),
            };

            _appDbContext.People.AddRange(new[] { admin, manager, employee1, employee2 });
            await _appDbContext.SaveChangesAsync();

            var application = new AnnualLeaveApplication
            {
                CreatedById = employee1.Id,
                Date = utcNow.Date.AddDays(5),
            };

            _appDbContext.AnnualLeaveApplications.Add(application);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
