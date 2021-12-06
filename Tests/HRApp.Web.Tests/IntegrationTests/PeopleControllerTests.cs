using FluentAssertions;
using HRApp.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sanakan.Web.Tests;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TestCI.Web.Tests.IntegrationTests
{
    [TestClass]
    public partial class PeopleControllerTests
    {
        protected static HttpClient _client;

        public PeopleControllerTests()
        {
            var factory = new TestWebApplicationFactory();
            _client = factory.CreateClient();
        }

        public async Task Cleanup()
        {
            _client.Dispose();
        }

        [TestMethod]
        public async Task Should_Get_Employees()
        {
            var personId = 1ul;

            var people = await _client.GetFromJsonAsync<IEnumerable<Person>>($"{personId}/members");
            people.Should().NotBeNull();
            people.Should().HaveCount(2);
        }
    }
}
