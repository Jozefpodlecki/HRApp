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
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly Mock<IOptionsMonitor<JwtConfiguration>> _jwtConfigurationMock = new(MockBehavior.Strict);
        private readonly Mock<IUserRepository> _userRepositoryMock = new(MockBehavior.Strict);
        private readonly Mock<IPasswordHasher> _passwordHasherMock = new(MockBehavior.Strict);
        private readonly Mock<IJwtBuilder> _jwtBuilderMock = new(MockBehavior.Strict);

        public AuthControllerTests()
        {
            _controller = new AuthController(
                _jwtConfigurationMock.Object,
                _userRepositoryMock.Object,
                _passwordHasherMock.Object,
                _jwtBuilderMock.Object);
        }

        [TestMethod]
        public async Task Should_Return_Bad_Request()
        {
            var model = new Models.Login
            {
                Email = "test@test.com",
            };

            _userRepositoryMock
                .Setup(pr => pr.GetByEmailAsync(model.Email))
                .ReturnsAsync(null as User);

            var result = await _controller.CreateToken(model);
            var badRequestObjectResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestObjectResult.Value.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Should_Return_Token_Data()
        {
            var model = new Models.Login
            {
                Email = "test@test.com",
                Password = "abc123",
            };
            var user = new User(model.Email);
            var passwordHash = new byte[10];

            var configuration = new JwtConfiguration
            {
                TokenExpiry = TimeSpan.FromHours(1),
            };

            var tokenData = new TokenData();

            _jwtConfigurationMock
                .Setup(pr => pr.CurrentValue)
                .Returns(configuration);

            _userRepositoryMock
                .Setup(pr => pr.GetByEmailAsync(model.Email))
                .ReturnsAsync(user);

            _passwordHasherMock
                .Setup(pr => pr.ComputeHash(model.Email, user.PasswordHashSalt))
                .Returns(passwordHash);

            _passwordHasherMock
                .Setup(pr => pr.Compare(user.PasswordHashSalt, user.PasswordHash))
                .Returns(true);

            _jwtBuilderMock
                .Setup(pr => pr.Build(configuration.TokenExpiry, It.IsAny<Claim>()))
                .Returns(tokenData);

            var result = await _controller.CreateToken(model);
            var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().BeEquivalentTo(tokenData);
        }
    }
}
