using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using PasswordManager.Api.Controllers;
using PasswordManager.Core;
using PasswordManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.Tests
{
    public class PasswordControllerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordController _controller;

        public PasswordControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new PasswordController(_context);
        }

        [Fact]
        public async Task GetPasswords_Should_Return_List()
        {
            _context.Passwords.Add(new PasswordEntry { Title = "Facebook", Username = "user1", EncryptedPassword = "password", Category = "Social", UserId = 1 });
            await _context.SaveChangesAsync();

            var result = await _controller.GetPasswords();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<PasswordEntry>>>(result);
            Assert.NotEmpty(actionResult.Value);
        }

        [Fact]
        public async Task AddPassword_Should_Add_Password()
        {
            var newPassword = new PasswordEntry { Title = "Google", Username = "user2", EncryptedPassword = "securepassword", Category = "Work", UserId = 1 };

            var result = await _controller.AddPassword(newPassword);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var addedPassword = Assert.IsType<PasswordEntry>(actionResult.Value);
            Assert.Equal("Google", addedPassword.Title);
        }

        [Fact]
        public async Task DeletePassword_Should_Remove_Password()
        {
            var password = new PasswordEntry { Title = "Netflix", Username = "user3", EncryptedPassword = "password", Category = "Streaming", UserId = 1 };
            _context.Passwords.Add(password);
            await _context.SaveChangesAsync();

            var result = await _controller.DeletePassword(password.Id);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
