using FluentAssertions;
using HRApp.DAL;
using HRApp.DAL.Models;
using HRApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestCI.Tests
{
    [TestClass]
    public class PersonRepositoryTests
    {
        private readonly AppDbContext _dbContext;
        private readonly IPersonRepository _repository;

        public PersonRepositoryTests()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(opt => 
            opt.UseInMemoryDatabase("database", x => x.UseHierarchyId()));
            services.AddScoped<IPersonRepository, PersonRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
            _repository = serviceProvider.GetRequiredService<IPersonRepository>();
        }

        [TestMethod]
        public async Task Should_Return_People()
        {
            var account = new Account("Company A");
            var testUser = new User("test@test.com");
            var testSubordinateUser = new User("test@test.com");

            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();

            _dbContext.Users.Add(testUser);
            _dbContext.Users.Add(testSubordinateUser);
            await _dbContext.SaveChangesAsync();

            var testPerson = new Person()
            {
                Id = testUser.Id,
                HierarchyId = HierarchyId.Parse("/"),
                AccountId = account.Id,
            };

            var testSubordinate = new Person()
            {
                Id = testSubordinateUser.Id,
                HierarchyId = HierarchyId.Parse("/1/"),
                AccountId = account.Id,
            };

            _dbContext.People.Add(testPerson);
            _dbContext.People.Add(testSubordinate);
            await _dbContext.SaveChangesAsync();

            var expected = await _repository.GetForPersonIdAsync(testPerson.Id);
            expected.Should().HaveCount(2);
            expected.First().Should().BeEquivalentTo(testSubordinate);
        }
    }
}
