using System.Threading.Tasks;
using Moq;
using Xunit;
using PasswordManager.Api.Services;
using PasswordManager.Core;
using PasswordManager.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManager.Tests
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["Jwt:Key"]).Returns("MaSuperCleUltraSecrete123456789012345!");
            _config = configMock.Object;

            _authService = new AuthService(_context, _config);
        }

        [Fact]
        public async Task Register_Should_Create_User()
        {
            var user = await _authService.Register("testuser", "password");

            Assert.NotNull(user);
            Assert.Equal("testuser", user.Username);
        }

        [Fact]
        public async Task Register_Should_Return_Null_If_User_Exists()
        {
            await _authService.Register("existinguser", "password");

            var result = await _authService.Register("existinguser", "password");

            Assert.Null(result);
        }

        [Fact]
        public async Task Login_Should_Return_Token_If_Credentials_Are_Correct()
        {
            await _authService.Register("testuser", "password");

            var token = await _authService.Login("testuser", "password");

            Assert.NotNull(token);
        }

        [Fact]
        public async Task Login_Should_Return_Null_If_Credentials_Are_Wrong()
        {
            await _authService.Register("testuser", "password");

            var token = await _authService.Login("testuser", "wrongpassword");

            Assert.Null(token);
        }
    }
}
