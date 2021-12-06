using FluentAssertions;
using HRApp.Common;
using HRApp.DAL.Models;
using HRApp.DAL.Repositories;
using HRApp.Web.Configuration;
using HRApp.Web.Controllers;
using HRApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRApp.Web.Tests.ControllerTests
{
    [TestClass]
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserRepository> _userRepositoryMock = new(MockBehavior.Strict);
        private readonly Mock<IUserContext> _userContextMock = new(MockBehavior.Strict);

        public UserControllerTests()
        {
            _controller = new UserController(
                _userRepositoryMock.Object,
                _userContextMock.Object);
        }

        [TestMethod]
        public async Task Should_Return_Roles()
        {
            var userId = Guid.NewGuid();

            var roles = new[]
            {
                new Role("Manager"),
            };

            _userContextMock
                .Setup(pr => pr.UserId)
                .Returns(userId);

            _userRepositoryMock
                .Setup(pr => pr.GetRolesForUserAsync(userId))
                .ReturnsAsync(roles);

            var result = await _controller.GetRolesForUserAsync();
            var okObjectResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            okObjectResult.Value.Should().BeEquivalentTo(roles);
        }
    }
}
