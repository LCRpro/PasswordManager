using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Api.Controllers;
using PasswordManager.Core;
using PasswordManager.Core.Models;
using Xunit;

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

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1") 
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

[Fact]
public async Task GetPasswords_Should_Return_List()
{
    var password = new PasswordEntry
    {
        Title = "Facebook",
        Username = "user1",
        EncryptedPassword = "password",
        Category = "Social",
        UserId = 1
    };

    _context.Passwords.Add(password);
    await _context.SaveChangesAsync();

    var result = await _controller.GetPasswords();

    var okResult = Assert.IsType<ActionResult<IEnumerable<PasswordEntry>>>(result);
    
    var objectResult = Assert.IsType<OkObjectResult>(okResult.Result);
    
    var passwords = Assert.IsType<List<PasswordEntry>>(objectResult.Value);

    Assert.NotEmpty(passwords);
}


        [Fact]
        public async Task AddPassword_Should_Add_Password()
        {
            var newPassword = new PasswordEntry 
            { 
                Title = "Google", 
                Username = "user2", 
                EncryptedPassword = "securepassword", 
                Category = "Work" 
            };

            var result = await _controller.AddPassword(newPassword);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var addedPassword = Assert.IsType<PasswordEntry>(actionResult.Value);
            Assert.Equal("Google", addedPassword.Title);
            Assert.Equal(1, addedPassword.UserId); 
        }

        [Fact]
        public async Task DeletePassword_Should_Remove_Password()
        {
            var password = new PasswordEntry 
            { 
                Title = "Netflix", 
                Username = "user3", 
                EncryptedPassword = "password", 
                Category = "Streaming", 
                UserId = 1 
            };
            _context.Passwords.Add(password);
            await _context.SaveChangesAsync();

            var result = await _controller.DeletePassword(password.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.Null(await _context.Passwords.FindAsync(password.Id)); 
        }
    }
}
