using FluentAssertions;
using HRApp.DAL.Models;
using HRApp.Web.Models;
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
    public partial class ApplicationControllerTests
    {
        protected static HttpClient _client;

        public ApplicationControllerTests()
        {
            var factory = new TestWebApplicationFactory();
            _client = factory.CreateClient();
        }

        public async Task Cleanup()
        {
            _client.Dispose();
        }

        [TestMethod]
        public async Task Should_Add_Annual_Leave_Application()
        {
            var application = new NewAnnualLeaveApplication
            {

            };

            var response = await _client.PostAsJsonAsync($"api/applications/annual-leave", application);
            response.Should().NotBeNull();
            response.EnsureSuccessStatusCode();
        }
    }
}
